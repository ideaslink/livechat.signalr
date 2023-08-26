(() => {
// window.onload = function() {
// support scroll to bottom of content div    
function scrollToView() {
    // alert("js")
    var contentctl = document.getElementById("content");
    contentctl.focus();
    contentctl.scrollTop = contentctl.scrollHeight;
};
})();