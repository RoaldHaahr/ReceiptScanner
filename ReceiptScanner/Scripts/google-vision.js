//var dateRegex = new RegExp(/(([0-3]?\d{1})\.([0-9]?\d{1})\.([1-2]\d{3}|\d{2}))|(([0-3]?\d)-([0-9]?\d{1})-([1-2]\d{3}|\d{2}))|(([0-3]?\d)\/([0-9]?\d{1})\/([1-2]\d{3}|\d{2}))/);
//var costsRegex = new RegExp(/\s-?\d*(, |\. |,|\.) ?\d{2}\s/g);
//var addressRegex = new RegExp(/([ÆæØøÅåA-Za-z]{1,} ){1,}([0-9]{1,3})/);
//var zipRegex = new RegExp(/\d{4} [ÆæØøÅåA-Za-z]{2,}( [ÆæØøÅåA-Za-z]{1,})?/);
//var nameRegex = new RegExp(/([ÆæØøÅåA-Za-z]* ?){1,}/);

//var CV_URL = 'https://vision.googleapis.com/v1/images:annotate?key=' + window.apiKey;


//$(function () {
//    $('#fileform').on('submit', uploadFiles);
//});

///**
// * 'submit' event handler - reads the image bytes and sends it to the Cloud
// * Vision API.
// */
//function uploadFiles(event) {
//    event.preventDefault(); // Prevent the default form post

//    // Grab the file and asynchronously convert to base64.
//    var file = $('#fileform [name=fileField]')[0].files[0];
//    var reader = new FileReader();
//    reader.onloadend = processFile;
//    reader.readAsDataURL(file);
//}

///**
// * Event handler for a file's data url - extract the image data and pass it off.
// */
//function processFile(event) {
//    var base64data = event.target.result;

//    $('img.preview').attr('src', base64data);
//    sendFileToCloudVision(base64data.replace('data:image/jpeg;base64,', ''));
//}

///**
// * Sends the given file contents to the Cloud Vision API and outputs the
// * results.
// */
//function sendFileToCloudVision(content) {
//    var type = $('#fileform [name=type]').val();

//    // Strip out the file prefix when you convert to json.
//    var request = {
//        requests: [{
//            image: {
//                content: content
//            },
//            features: [{
//                type: type,
//                maxResults: 200
//            }],
//            imageContext: {
//                languageHints: ["da"]
//            }
//        }]
//    };

//    $('#results').text('Loading...');
//    $.post({
//        url: "/Home/GetTextAsync",
//        data: JSON.stringify(request),
//        contentType: 'application/json'
//    }).fail(function (jqXHR, textStatus, errorThrown) {
//        $('#results').text(jqXHR.responseText);
//    }).done(displayJSON);
//}

//function displayJSON(data) {
//    var text = data.responses[0].fullTextAnnotation.text;
//    var words = data.responses[0].textAnnotations;
//    var contents = JSON.stringify(data, null, 4);
//    // var datePos = text.search(dateRegex);
//    var dateString = text.match(dateRegex)[0];
//    var date = getDate(dateString);
//    var name = text.match(nameRegex)[0];
//    // var name = getName(text);
//    var address = text.match(addressRegex)[0];
//    // var address = getInfo(text, addressRegex, /[0-9]{1,3}[!?0-9]\s/);
//    var streetNumber = address.match(/[0-9]{1,}/)[0];
//    var streetName = address.substring(0, address.length - (streetNumber.length + 1));
//    var postalArea = text.match(zipRegex)[0];
//    // var postalArea = getInfo(text, zipRegex, /\s/);
//    var zip = postalArea.match(/[0-9]{4}/)[0];
//    var city = postalArea.match(/([ÆæØøÅåA-Za-z]{1,} ?){1,}/)[0];
//    var costs = text.match(costsRegex).map(function (item) {
//        item = item.replace(/\s/g, "");
//        return parseFloat(item.replace(',', '.'));
//    });
//    var total = Math.max(...costs);
//    costs.forEach(function (cost) {
//        if (cost < 0) {
//            total += cost;
//        }
//    });

//    $('input[name=name]').val(name);
//    $('input[name=streetname]').val(streetName);
//    $('input[name=streetnumber]').val(streetNumber);
//    $('input[name=zip]').val(zip);
//    $('input[name=city]').val(city);
//    $('input[name=date]').val(date);
//    $('input[name=total]').val(total);
//    $('#results').html(text);
//    words.splice(1, words.length - 1).forEach(word => $('ul.words').append('<li>' + word.description + '</li>'));
//    var evt = new Event('results-displayed');
//    evt.results = contents;
//    document.dispatchEvent(evt);
//}

//function getInfo(text, regex, endRegex) {
//    var infoPos = text.search(regex),
//        endPos = text.substring(infoPos, text.length).search(endRegex) + infoPos + text.match(endPos).length;
//    return text.substring(infoPos, endPos);
//}

//function showPreview(imageData) {
//    $('img.preview').attr('src', imageData);
//}

//function getDate(dateString) {
//    var dd = dateString.substring(0, 2),
//        mm = dateString.substring(3, 5),
//        yyyy = "";
//    if (dateString.length > 10) {
//        return;
//    }
//    if (!Number.isInteger(parseInt(dateString[8]))) {
//        yyyy = "20" + dateString.substring(6, 8);
//    } else {
//        yyyy = dateString.substring(6, 10);
//    }
//    return yyyy + "-" + mm + "-" + dd;
//}

//function getName(text) {
//    namePos = text.search(new RegExp(/([ÆæØøÅåA-Za-z]* ?){1,}/));
//    spacePos = text.substring(namePos, text.length).indexOf("\n");
//    return text.substring(namePos, spacePos);
//}
