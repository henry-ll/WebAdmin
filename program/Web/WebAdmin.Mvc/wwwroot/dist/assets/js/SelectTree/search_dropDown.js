(function ($) {
  $.fn.hsCheckData = function (options) {
    var defaults = { data: null, disLevel: false, placeholder: "" };
    var opts = $.extend(defaults, options);
    $(this).addClass("hsCheckData");
    var id = $(this).attr("id");
    $("#" + id).text(opts.placeholder);
    $("#" + id).addClass("select-tree-placeholder");
    if (!id) {
      $(this).attr("id", "select-tree-id");
      id = "select-tree-id";
    }
    $(this).mouseenter(function (e) {
      if (
        $(this).attr("data-id") != "undefined" &&
        $(this).attr("data-id") != ""
      ) {
        var text = $(this).text();
        $(this).html(text + "<i class='select-tree-icon'></i>");
        $(".select-tree-icon").addClass("select-tree-delete-icon");
      }
    });
    $(this).mouseleave(function (e) {
      if ($(".select-tree-icon").has("select-tree-delete-icon")) {
        var text = $(this).text();
        $(this).html(text);
        $(".select-tree-icon").removeClass("select-tree-delete-icon");
      }
    });
      $(this).click(function (e) {
      if ($("#" + id + "_hcd").length > 0) {
        $("#" + id + "_hcd").remove();
        $("#" + id).removeClass("hsCjeckData_check");
        return false;
      }
      if ($(e.target).attr("class").indexOf("select-tree-delete-icon") > -1) {
        $("#" + id).removeClass("hsCjeckData_check");
        $("#" + id).attr("data-id", "");
        $("#" + id).attr("data-value", "");
        $("#" + id).addClass("select-tree-placeholder");
        $("#" + id).text(opts.placeholder);
        $("#" + id + "_hcd").remove();
        return false;
      }
      $(this).addClass("hsCjeckData_check");
      var mainHtml = "<div id='" + id + "_hcd' class='hcd_main_border'>";
      var default_id = $(this).attr("data-id");
      var filterHtml = filterHtmlFun();
      var dataListHtml = dataListHtmlFun(default_id);
      var btnHtml = buttonHtml();
      mainHtml += filterHtml + dataListHtml + btnHtml + "</div>";
      $("body").append(mainHtml);
      var width = $(this).outerWidth();
      var height = $(this).outerHeight();
      var x = $(this).offset().top;
      var y = $(this).offset().left;
      $("#" + id + "_input").css("width", "100%");
      $("#" + id + "_hcd")
        .css("width", width)
        .css("left", y)
        .css("top", x + height);
      $(".exsitChild").click(function () {
        if ($(this).parent().nextAll("ul").is(":hidden")) {
          $(this).addClass("exsitChild_check");
        } else {
          $(this).removeClass("exsitChild_check");
        }
        $(this).parent().nextAll("ul").toggle();
        return false;
      });
      $("[name='dataliCheck']").click(function (e) {
        e.stopPropagation();
      });
      $("[name='childcheckbox']").click(function (e) {
        e.stopPropagation();
      });
      $(".select-tree-selected").click(function (e) {
        e.stopPropagation();
        var data_id = "";
        var data_value = "";
        data_id = $(this).data("id");
        data_value = $(this).text().trim();
        $("#" + id).attr("data-id", data_id);
        $("#" + id).attr("data-value", data_value);
        $("#" + id).removeClass("select-tree-placeholder");
        $("#" + id).html(data_value);
        if (opts.disLevel) {
          if ($(this).hasClass("first-category")) {
            $("#" + id).attr("data-level", 1);
          } else if ($(this).hasClass("middle-category")) {
            $("#" + id).attr("data-level", 2);
          } else if ($(this).hasClass("third-category")) {
            $("#" + id).attr("data-level", 3);
          } else {
            $("#" + id).attr("data-level", "");
          }
        }
        $("#" + id + "_hcd").remove();
        $("#" + id).removeClass("hsCjeckData_check");
        return false;
      });
      $("#" + id + "_input").keyup(function () {
        if ($(this).val() != "") {
          var filterHtml = "<ul>";
          var getFilterHtml = getFilterHtmlFun($(this).val());
          filterHtml += getFilterHtml + "</ul>";
          $("#" + id + "_hcd")
            .children(".hcd_dataList")
            .html(filterHtml);
          $(".select-tree-selected").click(function (e) {
            e.stopPropagation();
            var data_id = "";
            var data_value = "";
            data_id = $(this).data("id");
            data_value = $(this).text().trim();
            $("#" + id).attr("data-id", data_id);
            $("#" + id).attr("data-value", data_value);
            $("#" + id).removeClass("select-tree-placeholder");
            $("#" + id).html(data_value);
            if (opts.disLevel) {
              if ($(this).hasClass("first-category")) {
                $("#" + id).attr("data-level", 1);
              } else if ($(this).hasClass("middle-category")) {
                $("#" + id).attr("data-level", 2);
              } else if ($(this).hasClass("third-category")) {
                $("#" + id).attr("data-level", 3);
              } else {
                $("#" + id).attr("data-level", "");
              }
            }
            $("#" + id + "_hcd").remove();
            $("#" + id).removeClass("hsCjeckData_check");
            return false;
          });
        } else {
          $("#" + id + "_hcd")
            .children(".hcd_dataList")
            .html(dataListHtmlFun());
        }
      });
      return false;
    });
    setShowData();
    $(document).click(function (e) {
      var clickEle = $(e.target).attr("id");
      var clickName = $(e.target).attr("name");
      if (
        clickEle == id + "_input" ||
        clickEle == id + "_hcd" ||
        clickName == "datali"
      ) {
        return false;
      }
      $("#" + id + "_hcd").remove();
      $("#" + id).removeClass("hsCjeckData_check");
    });
    function filterHtmlFun() {
      var html = "<div class='hcd_filter'>";
      html +=
        "<input autofocus='autofocus' type='text' id='" +
        id +
        "_input' class='hcd_filter_input'/>";
      html += "</div>";
      return html;
    }
    function dataListHtmlFun() {
      var html = "<div class='hcd_dataList'>";
      var data = getDataHtml();
      html += data + "</div>";
      return html;
    }
    function setShowData() {
      if (
        $("#" + id).attr("data-id") != undefined &&
        $("#" + id).attr("data-id") != ""
      ) {
        var sid = $("#" + id).attr("data-id"),
          text = getName(opts.data, sid);
        $("#" + id).removeClass("select-tree-placeholder");
        $("#" + id).text(text);
      }
    }
    function getName(json, jsonId) {
      var text = "";
      for (var i = 0; i < json.length; i++) {
        for (var key in json[i]) {
          if (key == jsonId) {
            text = json[i][key];
            return text;
          } else {
            text = getMiddleName(json[i][key], jsonId);
          }
        }
      }
      return text;
    }
    function getMiddleName(json, jsonId) {
      var text = "";
      for (var i = 0; i < json.length; i++) {
        for (var key in json[i]) {
          if (key == jsonId) {
            text = json[i][key];
            return text;
          } else {
            text = getThirdName(json[i][key], jsonId);
          }
        }
      }
      return text;
    }
    function getThirdName(json, jsonId) {
      var text = "";
      for (var i = 0; i < json.length; i++) {
        for (var key in json[i]) {
          if (key == jsonId) {
            text = json[i][key];
          }
        }
      }
      return text;
    }
    function getDataHtml(json) {
      var html = "<ul>";
      var default_id = $("#" + id).attr("data-id");
      var data = json || opts.data;
      var childIsNUll = true;
      var className = "";
      for (var i = 0; i < data.length; i++) {
        if ("children" in data[i]) {
          childIsNUll = false;
        }
        for (var key in data[i]) {
          if (data[i][key].toString().indexOf("object") == -1) {
            if (key == default_id) {
              className = "default_selected";
            } else {
              className = "";
            }
            if (childIsNUll == false) {
              html +=
                "<li class='parent select-tree-item' name='datali'><div class='parent_title' name='datali'><a class='exsitChild exsitChild_check'></a>";
              html +=
                "<span class='select-tree-selected first-category " +
                className +
                "' data-id='" +
                key +
                "'>" +
                data[i][key] +
                "</span></div> ";
              childIsNUll = true;
            } else {
              html +=
                "<li class='parent select-tree-item' name='datali'><div class='parent_title' name='datali'>";
              html +=
                "<span class='select-tree-selected first-category " +
                className +
                "'  data-id='" +
                key +
                "'>" +
                data[i][key] +
                "</span>";
              html += "</div></li>";
            }
          } else {
            html += childDataHtml(data[i][key]);
            html += "</li>";
          }
        }
      }
      html += "</ul>";
      return html;
    }
    function childDataHtml(json) {
      var html = "<ul class='select-tree-triggle'>";
      var childIsNUll = true;
      var className = "";
      var default_id = $("#" + id).attr("data-id");
      for (var i = 0; i < json.length; i++) {
        if ("children" in json[i]) {
          childIsNUll = false;
        }
        for (var key in json[i]) {
          if (json[i][key].toString().indexOf("object") == -1) {
            if (key == default_id) {
              className = "default_selected";
            } else {
              className = "";
            }
            if (childIsNUll == false) {
              html +=
                "<li class='middle select-tree-item' name='datali'><div class='middle_title' name='datali'><a class='exsitChild exsitChild_check'></a>";
              html +=
                "<span class='select-tree-selected middle-category " +
                className +
                "'  data-id='" +
                key +
                "'>" +
                json[i][key] +
                "</span></div > ";
              childIsNUll = true;
            } else {
              html +=
                "<li class='middle select-tree-item' name='datali'><div class='middle_title'><div class='middle_title' name='datali'><a style='visibility:hidden' class='exsitChild exsitChild_check'></a>";
              html +=
                "<span class='select-tree-selected middle-category " +
                className +
                "' data-id='" +
                key +
                "'>" +
                json[i][key] +
                "</span>";
              html += "</div></li>";
            }
          } else {
            html += childThirdHtml(json[i][key]);
            html += "</li>";
          }
        }
      }
      html += "</ul>";
      return html;
    }
    function childThirdHtml(json) {
      var default_id = $("#" + id).attr("data-id");
      var className = "";
      var html = "<ul class='select-tree-triggle'>";
      for (var i = 0; i < json.length; i++) {
        for (var key in json[i]) {
          if (key == default_id) {
            className = "default_selected";
          } else {
            className = "";
          }
          html +=
            "<li class='third select-tree-item' name='datali'><div class='third_title' name='datali'>";
          html +=
            "<span class='select-tree-selected third-category " +
            className +
            "' data-id='" +
            key +
            "'>" +
            json[i][key] +
            "</span></div ></li > ";
        }
      }
      return html + "</ul>";
    }
    function buttonHtml() {
      var html = "<div class='hcd_btn_div'>";
      html +=
        "<button type='button' id='" +
        id +
        "_btn' class='hcd_btn'>确定</button>";
      html += "</div>";
      return html;
    }
    function getFilterHtmlFun(filterInput) {
      var default_id = $("#" + id).attr("data-id");
      var className = "";
      var html = "";
      for (var i = 0; i < opts.data.length; i++) {
        for (var key in opts.data[i]) {
          if (key == default_id) {
            className = "default_selected";
          } else {
            className = "";
          }
          if (opts.data[i][key].indexOf(filterInput) > -1) {
            if (
              opts.data[i]["children"] &&
              opts.data[i]["children"].toString().indexOf("object") != -1
            ) {
              html += getDataHtml([opts.data[i]]);
            } else {
              html +=
                "<li class='select-tree-item' name='datali'><div class='parent_title' name='datali'>";
              html +=
                "<span class='select-tree-selected first-category " +
                className +
                "'  data-id='" +
                key +
                "'>" +
                opts.data[i][key] +
                "</span></div ></li > ";
            }
          } else {
            html += getChildFilterHtmlFun(opts.data[i][key], filterInput);
          }
        }
      }
      if (!html) {
        html += "<span class='select-tree-result'>Not Found</span>";
      }
      return html;
    }
    function getChildFilterHtmlFun(json, filterInput) {
      var html = "";
      var default_id = $("#" + id).attr("data-id");
      var className = "";
      for (var i = 0; i < json.length; i++) {
        for (var key in json[i]) {
          if (json[i][key].indexOf(filterInput) > -1) {
            if (key == default_id) {
              className = "default_selected";
            } else {
              className = "";
            }
            if (
              json[i]["children"] &&
              json[i]["children"].toString().indexOf("object") != -1
            ) {
              html += childDataHtml([json[i]]);
            } else {
              html +=
                "<li class='select-tree-item' name='datali'><div class='middle_title' name='datali'>";
              html +=
                "<span class='select-tree-selected middle-category " +
                className +
                "' data-id='" +
                key +
                "'>" +
                json[i][key] +
                "</span></div ></li > ";
            }
          } else {
            html += getChildThirdHtmlFun(json[i][key], filterInput);
          }
        }
      }
      return html;
    }
    function getChildThirdHtmlFun(json, filterInput) {
      var html = "";
      var default_id = $("#" + id).attr("data-id");
      var className = "";
      for (var i = 0; i < json.length; i++) {
        for (var key in json[i]) {
          if (key == default_id) {
            className = "default_selected";
          } else {
            className = "";
          }
          if (json[i][key].indexOf(filterInput) > -1) {
            html +=
              "<li class='select-tree-item' name='datali'><div class='third_title' name='datali'>";
            html +=
              "<span class='select-tree-selected third-category " +
              className +
              "' data-id='" +
              key +
              "'>" +
              json[i][key] +
              "</span></div ></li > ";
          }
        }
      }
      return html;
    }
  };
})(jQuery);
