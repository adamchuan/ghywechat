/*! 
//@ 自己整理的一些页面优化js
*/

//异步加载js url为要加载的文件地址 fn为加载成功时执行的函数
function loadScript(url, fn, doc, charset) {

    doc = doc || document;

    var script = doc.createElement("script");

    script.language = "javascript";

    script.charset = charset ? charset : 'utf-8';

    script.type = 'text/javascript';

    script.onload = script.onreadystatechange = function () {

        if (!script.readyState || 'loaded' === script.readyState || 'complete' === script.readyState) {

            fn && fn();

            script.onload = script.onreadystatechange = null;

            // script.parentNode.removeChild(script);
        };
    };
    script.src = url;

    document.body.appendChild(script);
}