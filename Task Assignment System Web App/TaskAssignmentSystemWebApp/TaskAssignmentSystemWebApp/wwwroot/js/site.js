var modal, loading;

function startLoadingIndicator() {
    modal = document.createElement("DIV");
    modal.className = "modalLoadingIndicator";
    document.body.appendChild(modal);
    loading = document.getElementsByClassName("loading")[0];
    loading.style.display = "block";
    var top = Math.max(window.innerHeight / 2 - loading.offsetHeight / 2, 0);
    var left = Math.max(window.innerWidth / 2 - loading.offsetWidth / 2, 0);
    loading.style.top = top + "px";
    loading.style.left = left + "px";
};

function stopLoadingIndicator() {
    setTimeout(function () {
        document.body.removeChild(modal);
        loading.style.display = "none";
    }, 0); //Delay just used for example and must be set to 0.
}

$(document)
    .ajaxStart(function () {
        var loadingIndicator = document.getElementsByClassName("loading")[0];
        //console.log("Start: " + loadingIndicator.style.display);
        if (loadingIndicator.style.display == "none") {
            //Start Loading Indicator
            startLoadingIndicator();
        }
    })
    .ajaxStop(function () {
        var loadingIndicator = document.getElementsByClassName("loading")[0];
        //console.log("Stop: " + loadingIndicator.style.display);
        if (loadingIndicator.style.display == "block") {
            //Stop Loading Indicator
            stopLoadingIndicator();
        }
    });
