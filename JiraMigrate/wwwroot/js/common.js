$(document).ready(() => {
    initializeSelect2ForAll();
})

function initializeSelect2ForAll() {
    $('select.select-2').each(function () {
        initializeSelect2ForDropdown(this)
    })
}

function initializeSelect2ForDropdown(source) {
    $(source).select2();
}

async function commonAjaxCall(request) {
    $.ajax({
        ...request,
        success: (response) => {
            request.success(response)
        },
        error: (error) => {
            alert('Something went wrong!')
            console.error(error)
        }
    })
}

function loadDropdownAjax(request) {
    let { id, url, htmlTemplate, isLazyload } = request;
    if (typeof (htmlTemplate) != 'function') {
        htmlTemplate = data => `<option value="${data.id}">${data.text}</option>`
    }
    if (!id) {
        throw 'Id not passed for loadDropdownAjax'
    }
    if (!url) {
        throw 'url not passed for loadDropdownAjax'
    }

    commonAjaxCall({
        url,
        success: (response) => {
            $('#' + id).html('');
            $('#' + id).html(response.data.map(x => htmlTemplate(x)).join(''));
        }
    })
}

const CommonNotifier = {
    showInformation: (msg, onConfirm) => {
        showModal('Information', msg, onConfirm, onConfirm)
    },
    showError: (msg, onConfirm) => {
        showModal('Error', msg, onConfirm, onConfirm)
    },
    showConfirmationWithCallback: (msg, onConfirm, onCancel) => {
        showModal('Confirm', msg, onConfirm, onCancel)
    },
    showConfirmationWithPromise: (msg) => new Promise((resolve, reject) => {
        showModal('Confirm', msg, function () {
            resolve(true)
        }, function () {
            resolve(false)
        })
    })
}


function showModal(heading, body, onOk, onCancel) {
    if (onCancel) {
        $('#common-modal .common-cancel').click(function () {
            onCancel();
        })
    }
    if (onOk) {
        $('#common-modal .common-ok').click(function () {
            onOk();
        })
    }

    $('#modalheader').text(heading);
    $('#modalBody').text(body);
    $('#common-modal').modal('show')
}