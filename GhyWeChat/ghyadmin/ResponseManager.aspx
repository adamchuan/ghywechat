<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponseManager.aspx.cs" Inherits="GhyWeChat.ghyadmin.ResponseManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自动回复管理</title>
    <style type="text/css">
        .editorblock
        {
            display:none;
        }
        .pager
        {
            display:none;
        }
        .nowpage
        {
            width:20px;
        }
    </style>
</head>
<body>
<a attr-type="1" class="addresponse">关键字回复</a>
<a attr-type="2" class="addresponse">被关注时回复</a>
<a attr-type="3" class="addresponse">自动回复</a>

<div class="editorblock">
<a class="addtext">使用文字素材</a>
<a class="addmusic">使用音乐素材</a>
<a class="addpic">使用图片素材</a>
<div class="opeateblock"></div>
<div class="selectblock">
</div>
<div class="pager"> 
  <a class="pagecontrol" method="pre">上一页</a><input type="text" class="nowpage"></span>/<span class="totalpage"></span><a class="pagecontrol" method="trans">跳转</a><a class="pagecontrol" method="next">下一页</a>
</div>
</div>
<div class="show"></div>
</body>
<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var editmode; //表示当前是用文字(1)，图片(2)还是音乐(3)素材
        $(".addresponse").click(function () {

            $(".opeateblock").html("");
            editmode = null; //清空编辑的状态
            $(".selectblock").html("");
            $(".editorblock").css("display", "block");
            $(".addresponse").css("background", "#fff");
            $(".pager").css("display", "none");
            $(this).css("background", "gray");
            switch ($(this).attr("attr-type")) {
                case "1":
                    $(".opeateblock").append("关键字<input type='text' class='key' /><br/>");

                    var addbutton = document.getElementById("addbutton");
                    if (addbutton == null) {
                        $(".opeateblock").append("<input type='button' id='addbutton' value='添加' />");
                    }

                    $("#addbutton").click(function () {
                        AddKeyResponse();
                    });
                    break;
                case "2":
                case "3":
                    var editbutton = document.getElementById("editbutton");
                    if (editbutton == null) {
                        $(".opeateblock").append("<input type='button' id='editbutton' method='" + $(this).attr("attr-type") + "' value='修改' />");
                    }
                    $("#editbutton").click(function () {
                        EditIntroduceOrAutoReply($("#editbutton").attr("method"));
                    });
                    break;

                default: break;
            }

        });
        $(".pagecontrol").click(function () {
            var page;
            var nowpage = parseInt($(".nowpage").val());
            switch ($(this).attr("method")) {
                case "pre":
                    page = nowpage - 1;
                    break;
                case "next":
                    page = nowpage + 1;
                    break;
                case "trans":
                    page = nowpage;
                    break;
                default:
                    break;
            }
            gotopage(page);
        });
        function EditIntroduceOrAutoReply(edittype) {
            if (editmode == null)
                return;
            var msgid = null;
            var radio = document.getElementsByName("textid");
            var i;
            for (i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    msgid = radio[i].value;
                    break;
                }
            }
            if (msgid == null) {
                alert("请选择素材");
                return;
            }
            $.ajax({
                type: "Post",
                url: "ResponseManager.aspx/EditIntroduceOrAutoReply",
                data: "{'msgid':'" + msgid + "','msgtype':'" + editmode + "','edittype':'" + edittype + "'}",
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    switch (data.d) {
                        case "1":
                            alert("添加成功");
                            break;
                        case "2":
                            alert("程序出错，添加失败");
                            break;
                        case "4":
                            alert("关键字已经存在");
                            break;
                        case "5":
                            alert("msgid错误");
                            break;
                        case "6":
                            alert("msgtype错误")
                            break;
                        case "7":
                            self.location = "Login.aspx";
                            alert("请先登录");
                            break;
                        default:
                            break;
                    }
                },
                error: function (err) {
                    alert("修改出错");
                }
            });
        }
        function AddKeyResponse() {
            if (editmode == null)
                return;
            var key = $(".key").val();
            if ($.trim(key) == "") {
                alert("请输入关键字");
                return;
            }
            var msgid = null;
            var radio = document.getElementsByName("textid");
            var i;
            for (i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    msgid = radio[i].value;
                    break;
                }
            }
            if (msgid == null) {
                alert("请选择素材");
                return;
            }
            $.ajax({
                type: "Post",
                url: "ResponseManager.aspx/AddKeyResponse",
                data: "{'msgid':'" + msgid + "','msgtype':'" + editmode + "','key':'" + key + "'}",
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    switch (data.d) {
                        case "1":
                            alert("添加成功");
                            break;
                        case "2":
                            alert("程序出错，添加失败");
                            break;
                        case "4":
                            alert("关键字已经存在");
                            break;
                        case "5":
                            alert("msgid错误");
                            break;
                        case "6":
                            alert("msgtype错误")
                            break;
                        case "7":
                            self.location = "Login.aspx";
                            alert("请先登录");
                            break;
                        default:
                            break;
                    }
                },
                error: function (err) {
                    alert("添加关键字出错");
                }
            });
        }
        function gotopage(page) {
            var pagesize = 10;
            var totalpage = $(".totalpage").html();
            if (page > totalpage) {
                alert("已经是最后页");
            }
            else if (page <= 0) {
                alert("已经第一页");
            }
            else {
                var msg = GetTextList(page, pagesize);
                CreateTextListView(msg);
                $(".nowpage").val(page);
            }
        }

        function GetTextList(pagenum, pagesize) {
            var getdata = "{'pagenum':'" + pagenum + "','pagesize':'" + pagesize + "'}";
            var d;
            $.ajax({
                type: "Post",
                url: "ResponseManager.aspx/GetTextList",
                data: getdata,
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                async: false, //是否为当前的请求触发全局AJAX事件处理函数  
                beforeSend: function () {
                    $(".show").html("加载中");
                },
                success: function (data) {
                    d = data.d;
                },
                error: function (err) {
                    $(".show").html("错误" + err);
                }
            });

            return eval('(' + d + ')')[0];
        }
        function CreateTextListView(msg) {
            $(".show").html("");
            $(".selectblock").html("");

            var i = 0;
            var html = "";
            for (i = 0; i < msg.count; i++) {
                html += "<p>" + "<input type='radio' name='textid' value='" + msg.textlist[i].textid + "'/>" + msg.textlist[i].text + "</p>";
            }
            $(".selectblock").html(html);
        }
        $(".addtext").click(function () {
     
            editmode = 1;
            var pagesize = 10;
            var pagenum = 1;
            var totalpage;
            $.ajax({
                type: "Post",
                url: "ResponseManager.aspx/GetTotalNum",
                data: "{'msgtype':'1'}",
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var num = data.d;

                    $(".pager").css("display", "block");
                    if (num % pagesize == 0)
                        totalpage = parseInt(num / pagesize);
                    else
                        totalpage = parseInt(num / pagesize + 1);
                    $(".totalpage").html(totalpage);
                    $(".nowpage").val(pagenum);

                }
            });
            var msg = GetTextList(pagenum, pagesize);
            CreateTextListView(msg);
        });

    });
</script>
</html>
