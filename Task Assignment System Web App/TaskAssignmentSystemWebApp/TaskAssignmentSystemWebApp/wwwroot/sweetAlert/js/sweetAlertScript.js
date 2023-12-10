function showAlertDialog({ icon = 'success', title = null, textMsg = null, timeOutDurationMS = 300, functionToCall = null, functionArgs = null, userControllerForTreeView = false, userControllerTreeViewLastArgs = null } = {}) {
    Swal.fire({
        icon: icon,
        title: title,
        text: textMsg,
        allowOutsideClick: true,
        allowEscapeKey: false,
        allowEnterKey: true,
        confirmButtonColor: '#0c63e4'
    }).then((result) => {
        if (userControllerForTreeView == true) {
            setTimeout(() => {
                functionToCall(functionArgs, "", "", userControllerTreeViewLastArgs);
            }, timeOutDurationMS);
        }
        else {
            setTimeout(() => {
                if (typeof functionToCall !== 'object' && functionToCall !== null) {
                    functionToCall(functionArgs);
                }
            }, timeOutDurationMS);
        }
    })
}

function showDeleteConfirmationDialog({ icon = 'warning', title = null, textMsg = null, functionToCall = null, functionArgs = null, showCancelButton = true, } = {}) {
    Swal.fire({
        icon: icon,
        title: title,
        text: textMsg,
        allowOutsideClick: true,
        allowEscapeKey: false,
        allowEnterKey: true,
        confirmButtonColor: "#ff0000",
        showCancelButton: showCancelButton,
        cancelButtonColor: "#0c63e4"
    }).then((result) => {
        if (result.isConfirmed) {
            if (typeof functionToCall !== 'object' && functionToCall !== null) {
                functionToCall(functionArgs);
            }
        }
    })
}