
var generateExcel = () => {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', `/Home/GenerateXLSX?fileName=document.xlsx`, true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], {
                type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            });
            var url = window.URL.createObjectURL(blob);
            var link = document.createElement('a');
            link.href = url;
            link.download = 'document.xlsx';
            document.body.appendChild(link);
            link.click();
            link.remove();
        }
    };
    xhr.send();
}