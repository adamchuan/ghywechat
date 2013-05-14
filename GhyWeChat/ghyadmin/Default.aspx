<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GhyWeChat._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GHYWeChat</title> 
</head>
<body>
  
    <input type="text" class="username" /><br />
    <input type="text" class="password" /><br />
    <input type="button" class="loginbutton" value="登录" /><br />
    <div class="show"></div>
   
</body>
<script type="text/javascript">
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
    loadScript("../Scripts/jquery-1.4.1.js", function () {  Login(); });
    function Login() {
        $(".loginbutton").click(function () {
            var username = $(".username").val();
            var password = $(".password").val();
            var getdata = "{'username':'" + username + "','password':'" + password + "'}";
            $.ajax({
                type: "Post",
                url: "Login",
                data: getdata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $(".show").html("加载中");
                },
                success: function (data) {
                    $(".show").html(data.d);
                },
                error: function (err) {
                    alert(getdata);
                    $(".show").html("错误" + err);
                }
            });
        });
    }
</script>
</html>
