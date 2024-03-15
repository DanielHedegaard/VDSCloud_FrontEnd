
window.downloadFileStream = function (fileId) {
    var link = document.createElement('a');
    link.href = 'https://localhost:7193/api/file?fileId=' + this.encodeURIComponent(fileId);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}