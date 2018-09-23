var dateRegex = new RegExp(/(([0-3]?\d{1})\.([0-9]?\d{1})\.([1-2]\d{3}|\d{2}))|(([0-3]?\d)-([0-9]?\d{1})-([1-2]\d{3}|\d{2}))|(([0-3]?\d)\/([0-9]?\d{1})\/([1-2]\d{3}|\d{2}))/);
var costsRegex = new RegExp(/\s-?\d*\.? ?\d*,\d{2}\s/g);
//var addressRegex = new RegExp(/([ÆæØøÅåA-Za-z]{1,} ){1,}([0-9]{1,3})/);
//var zipRegex = new RegExp(/\d{4} [ÆæØøÅåA-Za-z]{2,}( [ÆæØøÅåA-Za-z]{1,})?/);
var nameRegex = new RegExp(/([ÆæØøÅåA-Za-z]* ?){1,}/);

var b64Img = "";

var CV_URL = 'https://vision.googleapis.com/v1/images:annotate?key=' + window.apiKey;

// amounts to be rotated for the corresponding exif information
var rotation = {
    1: 0,
    3: 180,
    6: 90,
    8: 270
};

function _arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

function orientation(file, callback) {
    var fileReader = new FileReader();
    fileReader.onloadend = function () {
        var base64img = "data:" + file.type + ";base64," + _arrayBufferToBase64(fileReader.result);
        var scanner = new DataView(fileReader.result);
        var idx = 0;
        var value = 1; // Non-rotated is the default
        if (fileReader.result.length < 2 || scanner.getUint16(idx) != 0xFFD8) {
            // Not a JPEG
            if (callback) {
                callback(base64img, value);
            }
            return;
        }
        idx += 2;
        var maxBytes = scanner.byteLength;
        while (idx < maxBytes - 2) {
            var uint16 = scanner.getUint16(idx);
            idx += 2;
            switch (uint16) {
                case 0xFFE1: // Start of EXIF
                    var exifLength = scanner.getUint16(idx);
                    maxBytes = exifLength - idx;
                    idx += 2;
                    break;
                case 0x0112: // Orientation tag
                    // Read the value, its 6 bytes further out
                    // See page 102 at the following URL
                    // http://www.kodak.com/global/plugins/acrobat/en/service/digCam/exifStandard2.pdf
                    value = scanner.getUint16(idx + 6, false);
                    maxBytes = 0; // Stop scanning
                    break;
            }
        }
        if (callback) {
            callback(base64img, value);
        }
    };
    fileReader.readAsArrayBuffer(file);
}

$(function () {
    $('.js-receipt-upload').on('change', uploadFiles);
});

//
// 'submit' event handler - reads the image bytes and sends it to the Cloud
// Vision API.
//
function uploadFiles(event) {
    event.preventDefault(); // Prevent the default form post

    // Grab the file and asynchronously convert to base64.
    var file = $('.js-receipt-upload')[0].files[0];
    orientation(file, function (base64Image, value) {
        rotateBase64Image(base64Image, value, function (rotatedImage) {
            $('.receipt-information img').attr('src', rotatedImage);
            //sendFileToCloudVision(rotatedImage.replace('data:' + file.type + ';base64,', ''));
            b64Img = rotatedImage;
            sendFileToCloudVision(rotatedImage);
        });
    });
    // var reader = new FileReader();
    // reader.onloadend = processFile;
    // reader.readAsDataURL(file);
}

/**
* Event handler for a file's data url - extract the image data and pass it off.
* DELETE MAYBE
*/
function processFile(event) {
    var base64data = event.target.result;

    rotateBase64Image(base64data, 90, function (rotatedBase64Data) {
        $('.receipt-information img').attr('src', rotatedBase64Data);
        //sendFileToCloudVision(rotatedBase64Data.replace('data:image/jpeg;base64,', ''));
        sendFileToCloudVision(rotatedBase64Data);
    });

}

/**
* Sends the given file contents to the Cloud Vision API and outputs the
* results.
*/
function sendFileToCloudVision(content) {

    // Strip out the file prefix when you convert to json.
    //var request = {
    //    requests: [{
    //        image: {
    //            content: content
    //        },
    //        features: [{
    //            type: "TEXT_DETECTION",
    //            maxResults: 200
    //        }],
    //        imageContext: {
    //            languageHints: ["da"]
    //        }
    //    }]
    //};

    $('#results').text('Loading...');
    $.post({
        url: "/Receipt/Scan",
        dataType: "json",
        data: JSON.stringify({ base64String: content }),
        contentType: 'application/json'
    }).done(function (data) {
        displayJSON(JSON.parse(data));
    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#results').text('ERRORS: ' + textStatus + ' ' + errorThrown);
    });
}

function displayJSON(data) {
    var text = data.responses[0].fullTextAnnotation.text;
    //var words = data.responses[0].textAnnotations;
    var contents = JSON.stringify(data, null, 4);
    var dateString = text.match(dateRegex) ? text.match(dateRegex)[0] : null;
    var date = dateString ? getDate(dateString) : null;
    var name = text.match(nameRegex)[0];
    //var address = text.match(addressRegex)[0];
    //var streetNumber = address.match(/[0-9]{1,}/)[0];
    //var streetName = address.substring(0, address.length - (streetNumber.length + 1));
    //var postalArea = text.match(zipRegex)[0];
    //var zip = postalArea.match(/[0-9]{4}/)[0];
    //var city = postalArea.match(/([ÆæØøÅåA-Za-z]{1,} ?){1,}/)[0];
    var costs = text.match(costsRegex).map(function (item) {
        item = item.replace(/\s/g, "");
        item = item.replace(".", "");
        return parseFloat(item.replace(',', '.'));
    });
    var total = Math.max(...costs);
    costs.forEach(function (cost) {
        if (cost < 0) {
            total += cost;
        }
    });
    $('input[name=Base64_Image]').val(b64Img);
    $('input[name=Name]').val(name);
    //$('input[name=streetname]').val(streetName);
    //$('input[name=streetnumber]').val(streetNumber);
    //$('input[name=zip]').val(zip);
    //$('input[name=city]').val(city);
    $('input[name=Date]').val(date);
    $('input[name=Price]').val(total);
    $('textarea[name=ContentPreview]').html(text);
    $('input[name=Content]').val(text.replace(/\n/g, " "));
    document.getElementsByClassName("receipt-information")[0].style.display = "block";
    // words.splice(1, words.length-1).forEach(word => $('ul.words').append('<li>' + word.description + '</li>'));
    // var evt = new Event('results-displayed');
    // evt.results = contents;
    // document.dispatchEvent(evt);
}

function rotateBase64Image(base64Data, exifOrientation, callback) {
    var canvas = document.getElementById("c");
    var ctx = canvas.getContext("2d");
    var image = new Image();
    image.src = base64Data;
    image.onload = function () {

        if (exifOrientation == 6 || exifOrientation == 8) {
            canvas.height = image.width;
            canvas.width = image.height;
            ctx.translate(image.height / 2, image.width / 2);
        } else if (exifOrientation == 1 || exifOrientation == 3) {
            canvas.height = image.height;
            canvas.width = image.width;
            ctx.translate(image.width / 2, image.height / 2);
        }

        ctx.rotate(rotation[exifOrientation] * Math.PI / 180);
        ctx.drawImage(image, -image.width / 2, -image.height / 2);

        callback(canvas.toDataURL("image/jpeg"));
    };
}

function getInfo(text, regex, endRegex) {
    var infoPos = text.search(regex),
        endPos = text.substring(infoPos, text.length).search(endRegex) + infoPos + text.match(endPos).length;
    return text.substring(infoPos, endPos);
}

function showPreview(imageData) {
    $('img.preview').attr('src', imageData);
}

function getDate(dateString) {
    var dd = dateString.substring(0, 2),
        mm = dateString.substring(3, 5),
        yyyy = "";
    if (dateString.length > 10) {
        return;
    }
    if (!Number.isInteger(parseInt(dateString[8]))) {
        yyyy = "20" + dateString.substring(6, 8);
    } else {
        yyyy = dateString.substring(6, 10);
    }
    return yyyy + "-" + mm + "-" + dd;
}

function getName(text) {
    namePos = text.search(new RegExp(/([ÆæØøÅåA-Za-z]* ?){1,}/));
    spacePos = text.substring(namePos, text.length).indexOf("\n");
    return text.substring(namePos, spacePos);
}