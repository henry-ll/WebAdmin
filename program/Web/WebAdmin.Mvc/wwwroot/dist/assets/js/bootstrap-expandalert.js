var remind = {
    /**
     * 弹出消息框
     * @param msg 消息内容
     * @param type 消息框类型（参考bootstrap的alert）
     */
    alert: function (msg, type) {
        //debugger
        if (typeof (type) == "undefined") { // 未传入type则默认为success类型的消息框
            type = "success";
        }
        // 创建bootstrap的alert元素
        var divElement = $("<div></div>").addClass('alert').addClass('alert-' + type).addClass('alert-dismissible').addClass('show').addClass('fade');
        divElement.css({ // 消息框的定位样式
            "position": "absolute",
            "top": "80px",
            "padding": "14px 32%",
            "left": "50%",
            "height": "47px"
        });
        var divChildnodeElement = $("<div></div>").addClass('alert-body');
        // 消息框添加可以关闭按钮
        var closeBtn = $('<button class="close" data-dismiss="alert"><span>&times;</span></button>');
        divChildnodeElement.text(msg); // 设置消息框的内容
        $(divChildnodeElement).append(closeBtn);
        $(divElement).append(divChildnodeElement);
        // 消息框放入到页面中
        $('#textBoxs').append(divElement);
        return divElement;
    },

    /**
     * 短暂显示后上浮消失的消息框
     * @param msg 消息内容
     * @param type 消息框类型
     */
    message: function (msg, type) {
        var divElement = remind.alert(msg, type); // 生成Alert消息框
        var isIn = false; // 鼠标是否在消息框中

        divElement.on({ // 在setTimeout执行之前先判定鼠标是否在消息框中
            mouseover: function () { isIn = true; },
            mouseout: function () { isIn = false; }
        });

        // 短暂延时后上浮消失
        setTimeout(function () {
            var IntervalMS = 20; // 每次上浮的间隔毫秒
            var floatSpace = 40; // 上浮的空间(px)
            var nowTop = divElement.offset().top; // 获取元素当前的top值
            var stopTop = nowTop - floatSpace;    // 上浮停止时的top值
            divElement.fadeOut(IntervalMS * floatSpace); // 设置元素淡出

            var upFloat = setInterval(function () { // 开始上浮
                if (nowTop >= stopTop) { // 判断当前消息框top是否还在可上升的范围内
                    divElement.css({ "top": nowTop-- }); // 消息框的top上升1px
                } else {
                    clearInterval(upFloat); // 关闭上浮
                    divElement.remove();    // 移除元素
                }
            }, IntervalMS);

            if (isIn) { // 如果鼠标在setTimeout之前已经放在的消息框中，则停止上浮
                clearInterval(upFloat);
                divElement.stop();
            }

            divElement.hover(function () { // 鼠标悬浮时停止上浮和淡出效果，过后恢复
                clearInterval(upFloat);
                divElement.stop();
            }, function () {
                divElement.fadeOut(IntervalMS * (nowTop - stopTop)); // 这里设置元素淡出的时间应该为：间隔毫秒*剩余可以上浮空间
                upFloat = setInterval(function () { // 继续上浮
                    if (nowTop >= stopTop) {
                        divElement.css({ "top": nowTop-- });
                    } else {
                        clearInterval(upFloat); // 关闭上浮
                        divElement.remove();    // 移除元素
                    }
                }, IntervalMS);
            });
        }, 1500);
    }
}

/*
 * 提取通用的通知消息方法
 * @param $msg 提示信息
 * @param $type 提示类型:'info', 'success', 'warning', 'danger'
 * @param $delay 毫秒数，例如：1000
 * @param $icon 图标，例如：'fa fa-user' 或 'mdi mdi-alert'
 * @param $from 'top' 或 'bottom' 消息出现的位置
 * @param $align 'left', 'right', 'center' 消息出现的位置
 * @param $url 跳转链接，$delay毫秒后跳转  例如： https://www.xxxx.com
 */
showNotify = function ($msg, $type, $delay, $icon, $from, $align, $url) {
    $type = $type || 'info';
    $delay = $delay || 1000;
    $from = $from || 'top';
    $align = $align || 'left';
    $enter = $type == 'danger' ? 'animate__animated animate__shakeX' : 'animate__animated animate__fadeInUp';
    $url = $url || '';
 /*
 * @param icon: 消息图标，可以是一个字符串或者一个 HTML 元素。
 * @param message: 消息内容，可以是一个字符串或者一个 HTML 元素。
 * @param element: 通知消息所在的容器元素，默认是 body。
 * @param type: 消息类型，可以是 info、success、warning 或者 danger。
 * @param allow_dismiss: 是否允许用户手动关闭通知消息。
 * @param newest_on_top: 是否将最新的消息放在顶部。
 * @param showProgressbar: 是否显示进度条。
 * @param placement: 消息位置，包括 from 和 align 两个参数，可以设置为 top、bottom、left、right 等等。
 * @param offset: 消息离容器元素的偏移量。
 * @param spacing: 多个消息之间的间距。
 * @param z_index: 消息的 z-index 值。
 * @param delay: 消息显示的时间（毫秒），默认值是 5000 毫秒。
 * @param animate: 消息显示和隐藏时的动画效果，包括 enter 和 exit 两个参数，可以设置为各种 CSS 动画类名，如 animate__animated animate__bounceInRight。
 */
    jQuery.notify(
        {
            icon: $icon,
            message: $msg
        },
        {
            element: 'body',
            type: $type,
            allow_dismiss: true,
            newest_on_top: true,
            showProgressbar: false,
            placement: {
                from: $from,
                align: $align
            },
            offset: 20,
            spacing: 10,
            z_index: 10800,
            delay: $delay,
            animate: {
                enter: $enter,
                exit: 'animate__animated animate__fadeOutDown'
            }
        }
    );
    if ($url != '') {
        setTimeout(function () {
            window.location.href = $url;
        }, $delay);
    }
}