var allowedNum = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.'];

function formatNumber(number) {
    var parts = number.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    return parts.join(".");
}

function removeCommas(numberStr) {
    var lstLetters = numberStr;
    var lstReplace = lstLetters.replace(/\,/g, '');

    return lstReplace;
}

function alphanumeric(keyEvent) {
    var keyCode = keyEvent.keyCode || keyEvent.which;
    var regex = /^[a-zA-Z0-9_\ ]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));

    if (!isValid) {
        keyEvent.preventDefault();
    }
}

function alphanumericSpecial(keyEvent) {
    var keyCode = keyEvent.keyCode || keyEvent.which;
    var regex = /^[a-zA-Z0-9_!()\/\-\,\.\'\ ]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));

    if (!isValid) {
        keyEvent.preventDefault();
    }
}

function alpha(keyEvent) {
    var keyCode = keyEvent.keyCode || keyEvent.which;
    var regex = /^[a-zA-Z\ ]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));

    if (!isValid) {
        keyEvent.preventDefault();
    }
}

function alphaSpecial(keyEvent) {
    var keyCode = keyEvent.keyCode || keyEvent.which;
    var regex = /^[a-zA-Z_!()\/\-\,\.\'\ ]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));

    if (!isValid) {
        keyEvent.preventDefault();
    }
}

function numberOnly(keyEvent) {
    var keyCode = keyEvent.keyCode || keyEvent.which;
    var regex = /^[0-9]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));

    if (!isValid) {
        keyEvent.preventDefault();
    }
}

function decimalOnly(keyEvent) {
    var keyCode = keyEvent.keyCode || keyEvent.which;
    var regex = /^[0-9\.]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));

    if (!isValid) {
        keyEvent.preventDefault();
    }
}

function formatThousandSeparator(selector, keyEvent) {
    // skip for arrow keys
    if (keyEvent.which >= 37 && keyEvent.which <= 40) {
        keyEvent.preventDefault();
    }

    $(selector).val(function (index, value) {
        value = value.replace(/,/g, '');
        return formatNumber(value);
    });
}

function commaSeparator(element) {
    $(element).val(function (index, value) {
        if (value == "")
            return value;

        value = parseFloat(value.replace(/,/g, '')).toFixed(2);

        if (value == "NaN" || value == undefined)
            return 0.00;

        return formatNumber(value);
    });
}

// This function needs Sweetalert 2 to run
function showLoader(title = "") {
    Swal.fire({
        title: title,
        allowOutsideClick: false,
        allowEscapeKey: false,
        showConfirmButton: false,
        onBeforeOpen: () => { Swal.showLoading(); }
    });
}

// This function needs Sweetalert 2 to run
function showToastLoader(title = "") {
    Swal.fire({
        title: title,
        showConfirmButton: false,
        toast: true,
        onBeforeOpen: () => { Swal.showLoading(); }
    });
}

function browserPrint(data) {
    var mywindow = window.open('', 'my div', 'width=100');
    mywindow.document.write('<html><head>');
    mywindow.document.write('</head><body>');
    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10

    mywindow.print();
    mywindow.close();

    return true;
}

function filterTable(table, elementClass, search) {
    $(table).filter(function () {
        $(this).toggle($(this).find(elementClass).text().toLowerCase().indexOf(search) > -1);
    });
}

function addItemToArray(array, item) {
    array.push(item);
}

function removeItemFromArray(array, item, delCount) {
    array.splice(array.indexOf(item), delCount);
}

function validateAlpha(txt) {
    var regex = /^[a-zA-Z_\/\-\,\.\'\ ]+$/;

    return regex.test(txt);
}

function validateAlphanumeric(txt) {
    var regex = /^[a-zA-Z0-9_\/\-\,\.\'\ ]+$/;

    return regex.test(txt);
}

function validateDecimal(txt) {
    var _text = removeCommas(txt);
    var regex = /^[0-9\.]+$/;

    return regex.test(_text);
}

function actionLoader(show = true) {
    if (show) {
        $('#actionLoader').show();
    } else {
        setTimeout(() => { $('#actionLoader').hide(); }, 500);
    }
}

function postJson(url, data) {
    return $.ajax({
        url: url,
        method: 'POST',
        data: data,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    });
}
function postJson1(url, data = null, token) {
    return $.ajax({
        url: url,
        method: 'POST',
        headers: { 'Authorization': 'Bearer ' + token },
        contentType: 'application/json; charset=utf-8',
        data: data,
        dataType: 'json'
    });
}

function getJson(url, data = null) {
    return $.ajax({
        url: url,
        method: 'GET',
        data: data,
        dataType: 'json'
    });
}

function getJson(url, data = null, token) {
    return $.ajax({
        url: url,
        method: 'GET',
        headers: { 'Authorization': 'Bearer ' + token },
        data: data,
        dataType: 'json'
    });
}

function getHtml(url, data = null) {
    return $.ajax({
        url: url,
        method: 'GET',
        data: data,
        dataType: 'html'
    });
}

function deleteJson(url, data = null) {
    return $.ajax({
        url: url,
        method: 'DELETE',
        data: data,
        dataType: 'json'
    });
}

function deleteJson(url, data = null, token) {
    return $.ajax({
        url: url,
        method: 'DELETE',
        headers: { 'Authorization': 'Bearer ' + token },
        data: data,
        dataType: 'json'
    });
}

function resetSession() {
    $.ajax({
        url: '/session/reset',
        method: 'GET',
        cache: false,
        dataType: 'json'
    }).done(function (response) {
        console.log(response.status + ': session reset ' + response.txt);
    }).fail(function (xhr) {
        console.log(xhr.status);
    });
}

function stringLimiter(keyEvent, idName, limit) {
   
    var keyCode = keyEvent.keyCode || keyEvent.which;
    let tbValue = $("#" + idName).val();
    // need to add plus one because of the process
    if (tbValue.length + 1 > limit) {
        keyEvent.preventDefault();
    }
    //var regex = /^[0-9]+$/;
/*    alert(String.fromCharCode(keyCode));*/
    //var isValid = regex.test(String.fromCharCode(keyCode));

    //if (!isValid) {
    //    keyEvent.preventDefault();
    //}
}

function doughnutChartJs(ctx, type, labels, label, data) {

    var myChart = new Chart(ctx, {
        type: type,
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                backgroundColor: [
                    'rgba(103,162,201)',
                    'rgba(23,179,163)',
                    'rgba(227,123,123)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(103,162,201)',
                    'rgba(23,179,163)',
                    'rgba(227,123,123)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });

}

function concernBarChartJs(ctx, type, labels, label, data) {

    const barChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: label,
                    data: data,
                    backgroundColor: [
                        'rgba(103,162,201)',
                        'rgba(23,179,163)',
                        'rgba(227,123,123)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(153, 102, 122, 0.2)',
                        'rgba(153, 100, 200, 0.2)',
                    ],
                    borderWidth: 1
                },
                
            ]
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Months'
                    }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Data Values'
                    }
                }
            }
        }
    });

}

function concernMultipleBarChartJs(ctx, type, labels, data2D) {
    const barChart = new Chart(ctx, {

        type: type,
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Home Concerns',
                    data: data2D[0],
                    backgroundColor: [
                        'rgba(103,162,201)',
                        'rgba(23,179,163)',
                        'rgba(227,123,123)',
                        'rgba(75, 192, 192, 0.2)',
                    ],
                    borderWidth: 1
                },
                {
                    label: 'Moods/Behaviors',
                    data: data2D[1],
                    backgroundColor: [
                        'rgba(103,162,201)',
                        'rgba(23,179,163)',
                        'rgba(227,123,123)',
                        'rgba(75, 192, 192, 0.2)',
                    ],
                    borderWidth: 1
                },
                {
                    label: 'Academic Concerns',
                    data: data2D[2],
                    backgroundColor: [
                        'rgba(103,162,201)',
                        'rgba(23,179,163)',
                        'rgba(227,123,123)',
                        'rgba(75, 192, 192, 0.2)',
                    ],
                    borderWidth: 1
                },
                {
                    label: 'Relationship',
                    data: data2D[3],
                    backgroundColor: [
                        'rgba(103,162,201)',
                        'rgba(23,179,163)',
                        'rgba(227,123,123)',
                        'rgba(75, 192, 192, 0.2)',
                    ],
                    borderWidth: 1
                },
            ]
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Months'
                    }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Data Values'
                    }
                }
            }
        }
    });

}

