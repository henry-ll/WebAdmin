//确保jQuery已加载
if (typeof jQuery === "undefined") {
    throw new Error("MultiTabs requires jQuery");
} ((function ($) {
    "use strict";
    var NAMESPACE; //变量
    var MultiTabs, handler, getTabIndex, isExtUrl, sumDomWidth, trimText, supportStorage; //函数方法
    var defaultLayoutTemplates, defaultInit; //默认变量

    NAMESPACE = '.multitabs'; // on()函数的命名空间

    /**
     * 拼接on()函数的命名空间，并将其绑定
     * @param $selector        jQuery选择器
     * @param event             事件
     * @param childSelector     子选择器（字符串），与on()函数相同
     * @param fn                function
     * @param skipNS        bool值。如果为true，则跳过拼接名称空间
     */
    handler = function ($selector, event, childSelector, fn, skipNS) {
        var ev = skipNS ? event : event.split(' ').join(NAMESPACE + ' ') + NAMESPACE;
        $selector.off(ev, childSelector, fn).on(ev, childSelector, fn);
    };

    /**
     * 获取选项卡的索引
     * @param content 内容类型，对于“main”选项卡只能是第一个
     * @param url
     * @returns string return index
     */
    getTabIndex = function (content, url) {
        if (content === 'main') return 0;

        var tabIndex = window.btoa(url);
        tabIndex = tabIndex.replace(/\+/g, "_43");
        tabIndex = tabIndex.replace(/\//g, "_47");
        tabIndex = tabIndex.replace(/=/g, "_61");

        return tabIndex;
    };

    /**
     * 修剪文本，删除多余的空格，并使用maxLength修剪文本，添加“…”修剪后。
     * @param text          需要修剪的文本
     * @param maxLength     文本的最大长度
     * @returns {string}    返回修剪后的字符串
     */
    trimText = function (text, maxLength) {
        maxLength = maxLength || $.fn.multitabs.defaults.navTab.maxTitleLength;
        var words = (text + "").split(' ');
        var t = '';
        for (var i = 0; i < words.length; i++) {
            var w = $.trim(words[i]);
            t += w ? (w + ' ') : '';
        }

        if (t.length > maxLength) {
            t = t.substr(0, maxLength);
            t += '...'
        }
        return t;
    };

    supportStorage = function (is_cache) {
        var b = sessionStorage === undefined;
        var result = !b && is_cache;
        return result;
    }

    /**
     * 计算总宽度
     * @param JqueryDomObjList     用于计算的对象列表
     * @returns {number}        返回对象总宽度（int）
     */
    sumDomWidth = function (JqueryDomObjList) {
        var width = 0;
        $(JqueryDomObjList).each(function () {
            width += $(this).outerWidth(true)
        });
        return width;
    };

    /**
     * 判断是外部URL
     * @param url           用于判断的URL
     * @returns {boolean}   外部URL返回true，本地返回false
     */
    isExtUrl = function (url) {
        var absUrl = (
            function (url) {
                var a = document.createElement('a');
                a.href = url;
                return a.href;
            }
        )(url);
        var webRoot = window.location.protocol + '//' + window.location.host + '/';
        var urlRoot = absUrl.substr(0, webRoot.length);
        var result = (!(urlRoot === webRoot));
        return result;
    };

    /**
     * 布局模板
     */
    defaultLayoutTemplates = {
        /**
         * 主要布局
         */
        default: '<div class="mt-wrapper {mainClass}" style="height: 100%;" >' +
            '  <div class="mt-nav-bar {navClass}" style="background-color: {backgroundColor};">' +
            '    <div class="mt-nav mt-nav-tools-left">' +
            '      <ul  class="nav {nav-tabs}">' +
            '        <li class="nav-item mt-move-left"><a class="nav-link"><i class="mdi mdi-skip-backward"></i></a></li>' +
            '      </ul>' +
            '    </div>' +
            '    <nav class="mt-nav mt-nav-panel">' +
            '      <ul class="nav {nav-tabs}"></ul>' +
            '    </nav>' +
            '    <div class="mt-nav mt-nav-tools-right">' +
            '      <ul  class="nav {nav-tabs}">' +
            '        <li class="nav-item mt-move-right"><a class="nav-link"><i class="mdi mdi-skip-forward"></i></a></li>' +
            '        <li class="nav-item mt-dropdown dropdown">' +
            '          <a href="#" class="nav-link dropdown-toggle" data-toggle="dropdown" aria-expanded="false">{dropdown}<span class="caret"></span></a>' +
            '          <ul role="menu" class="dropdown-menu dropdown-menu-right" style="width: 0;text-align: center;">' +
            '            <li class="mt-show-actived-tab"><a class="dropdown-item">{showActivedTab}</a></li>' +
            '            <li class="mt-close-all-tabs"><a class="dropdown-item">{closeAllTabs}</a></li>' +
            '            <li class="mt-close-other-tabs"><a class="dropdown-item">{closeOtherTabs}</a></li>' +
            '          </ul>' +
            '        </li>' +
            '      </ul>' +
            '    </div>' +
            '  </div>' +
            '  <div class="tab-content mt-tab-content">' +
            '    <div id="lyear-loading"><div class="spinner-border" role="status"><span class="visually-hidden"></span></div></div>' +
            '  </div>' +
            '</div>',
        classic: '<div class="mt-wrapper {mainClass}" style="height: 100%;" >' +
            '  <div class="mt-nav-bar {navClass}" style="background-color: {backgroundColor};">' +
            '    <nav class="mt-nav mt-nav-panel">' +
            '      <ul  class="nav {nav-tabs}"> </ul>' +
            '    </nav>' +
            '    <div class="mt-nav mt-nav-tools-right">' +
            '      <ul  class="nav {nav-tabs}">' +
            '        <li class="mt-dropdown dropdown">' +
            '          <a href="#"  class="dropdown-toggle dropdown-item" data-toggle="dropdown">{dropdown}<span class="caret"></span></a>' +
            '          <ul role="menu" class="mt-hidden-list dropdown-menu dropdown-menu-right"></ul>' +
            '        </li>' +
            '      </ul>' +
            '    </div>' +
            '  </div>' +
            '  <div class="tab-content mt-tab-content " > </div>' +
            '</div>',
        simple: '<div class="mt-wrapper {mainClass}" style="height: 100%;" >' +
            '  <div class="mt-nav-bar {navClass}" style="background-color: {backgroundColor};">' +
            '    <nav class="mt-nav mt-nav-panel">' +
            '      <ul  class="nav {nav-tabs}"> </ul>' +
            '    </nav>' +
            '  </div>' +
            '  <div class="tab-content mt-tab-content " > </div>' +
            '</div>',
        navTab: '<a data-id="{navTabId}" class="nav-link mt-nav-tab" data-type="{type}" data-index="{index}" data-url="{url}">{title}</a>',
        closeBtn: ' <i class="mt-close-tab mdi mdi-close" style="{style}"></i>',
        ajaxTabPane: '<div id="{tabPaneId}" class="tab-pane {class}">{content}</div>',
        iframeTabPane: '<iframe id="{tabPaneId}" allowtransparency="true" class="tab-pane {class}" scrolling="yes" width="100%"  height:"100%"; frameborder="0" src="" seamless></iframe>'
    };

    /**
     * 默认初始化页
     * @type {*[]}
     */
    defaultInit = [
        //默认的初始化选项卡
        {
            type: 'main', //默认值为info
            title: 'main', //默认标题
            content: '<h1>Demo Page</h1><h2>Welcome to use bootstrap multi-tabs :) </h2>' //默认内容
        }
    ];

    /**
     * multitabs 构造函数
     * @param element       主容器
     * @param options       options
     */
    MultiTabs = function (element, options) {
        var self = this;
        self.$element = $(element);
        self._init(options)._listen()._final();
    };

    /**
     * MultiTabs's 函数
     */
    MultiTabs.prototype = {
        /**
         * 构造函数
         */
        constructor: MultiTabs,

        /**
         * 创建Tab选项卡并返回。
         * @param obj        要触发的multitabs对象
         * @param active       如果为true，则在创建后激活Tab选项卡
         * @returns this        Chain structure.
         */
        create: function (obj, active) {
            var options = this.options;
            var param;
            var $navTab;
            var getParam = this._getParam(obj);
            var b = !(param = getParam);
            if (b) {
                //如果 是无效的对象，则返回multitabs对象
                return this;
            }
            $navTab = this._exist(param)
            if ($navTab && !param.isNewTab) {
                this.active($navTab);
                return this;
            }
            param.active = !param.active ? active : param.active;
            //创建 nav tab 
            $navTab = this._createNavTab(param);
            //创建 tab-pane
            this._createTabPane(param);
            //添加tab到storage里去
            this._storage(param.did, param);
            if (param.active) {
                this.active($navTab);
            }
            return this;
        },

        /**
         * 创建 tab pane
         * @param param
         * @param index
         * @returns {*|{}}
         * @private
         */
        _createTabPane: function (param) {
            var self = this,
                $el = self.$element;
            $el.tabContent.append(self._getTabPaneHtml(param));
            var result = $el.tabContent.find('#' + param.did);
            return result;
        },

        /**
         * 获取 tab pane html
         * @param param
         * @param index
         * @returns {string}
         * @private
         */
        _getTabPaneHtml: function (param) {
            var self = this,
                options = self.options;
            if (!param.content && param.iframe) {
                var result = defaultLayoutTemplates.iframeTabPane.replace('{class}', options.content.iframe.class).replace('{tabPaneId}', param.did);
                return result;
            } else {
                var result = defaultLayoutTemplates.ajaxTabPane.replace('{class}', options.content.ajax.class).replace('{tabPaneId}', param.did).replace('{content}', param.content);
                return result;
            }
        },

        /**
         * 创建 nav tab
         * @param param
         * @param index
         * @returns {*|{}}
         * @private
         */
        _createNavTab: function (param) {
            var self = this, $el = self.$element;
            var navTabHtml = self._getNavTabHtml(param);
            var $navTabLi = $el.navPanelList.find('a[data-type="' + param.type + '"][data-index="' + param.index + '"]').parent('li');
            if ($navTabLi.length) {
                $navTabLi.html(navTabHtml);
                self._getTabPane($navTabLi.find('a:first')).remove(); //直接删除旧的 content pane
            } else {
                $el.navPanelList.append('<li class="nav-item">' + navTabHtml + '</li>');
            }
            var result = $el.navPanelList.find('a[data-type="' + param.type + '"][data-index="' + param.index + '"]:first');
            return result;

        },

        /**
         * 获取 nav tab html
         * @param param
         * @param index
         * @returns {string}
         * @private
         */
        _getNavTabHtml: function (param) {
            var self = this, options = self.options;
            var closeBtnHtml, display;

            display = options.nav.showCloseOnHover ? '' : 'display:inline;';
            closeBtnHtml = (param.type === 'main') ? '' : defaultLayoutTemplates.closeBtn.replace('{style}', display); //mian 页面 不能被关闭
            var result = defaultLayoutTemplates.navTab.replace('{index}', param.index).replace('{navTabId}', param.did).replace('{url}', param.url).replace('{title}', param.title).replace('{type}', param.type) + closeBtnHtml;
            return result;
        },

        /**
         * 生成tab pane's的id
         * @param param
         * @param index
         * @returns {string}
         * @private
         */
        _generateId: function (param) {
            var result = 'multitabs_' + param.type + '_' + param.index;
            return result;
        },

        /**
         * 活动的  选项卡
         * @param navTab
         * @returns self      Chain structure.
         */
        active: function (navTab, isNavBar) {
            var self = this, $el = self.$element;
            isNavBar = (isNavBar == false) ? false : true;
            var $navTab = self._getNavTab(navTab), $tabPane = self._getTabPane($navTab), $prevActivedTab = $el.navPanelList.find('li a.active');
            var prevNavTabParam = $prevActivedTab.length ? self._getParam($prevActivedTab) : {};
            var navTabParam = $navTab.length ? self._getParam($navTab) : {};
            //改变 storage active 的状态
            var storage = self._storage();
            if (storage[prevNavTabParam.did]) {
                storage[prevNavTabParam.did].active = false;
            }
            if (storage[navTabParam.did]) {
                storage[navTabParam.did].active = true;
            }
            self._resetStorage(storage);
            //active navTab and tabPane
            $prevActivedTab.removeClass('active');
            $navTab.addClass('active');
            self._fixTabPosition($navTab);
            self._getTabPane($prevActivedTab).removeClass('active');
            $tabPane.addClass('active');
            self._fixTabContentLayout($tabPane);
            //fill tab pane
            self._fillTabPane($tabPane, navTabParam, isNavBar);

            return self;
        },
        /**
         * fill tab pane
         * @private
         */
        _fillTabPane: function (tabPane, param, isNavBar) {
            var self = this, options = self.options;
            var $tabPane = $(tabPane);
            //if navTab-pane empty, load content
            var tabPanehtml = $tabPane.html();
            if (!tabPanehtml) {
                var isiframe = $tabPane.is('iframe');
                if (isiframe) {
                    var issrc = $tabPane.attr('src');
                    var isrefresh = options.refresh;
                    var paramurl = param.url;
                    if (!issrc || (!issrc && isrefresh == 'no') || (isrefresh == 'nav' && isNavBar) || isrefresh == 'all') {
                        $('#lyear-loading').fadeIn('fast', function () {
                            $tabPane.attr('src', paramurl);
                        });
                        if (!$tabPane[0].addEventListener) {
                            $tabPane[0].attachEvent('onload', function () {
                                callbackTheme($tabPane, function () {
                                    $('#lyear-loading').fadeOut('slow');
                                });
                            });
                        }
                        $tabPane[0].addEventListener('load', function () {
                            callbackTheme($tabPane, function () {
                                $('#lyear-loading').fadeOut('slow');
                            });
                        }, true);
                    }
                } else {
                    $.ajax({
                        url: param.url,
                        dataType: "html",
                        success: function (callback) {
                            $tabPane.html(options.content.ajax.success(callback));
                        },
                        error: function (callback) {
                            $tabPane.html(options.content.ajax.error(callback));
                        }
                    });
                }
            }
        },
        /**
         * move left
         * @return self
         */
        moveLeft: function () {
            var self = this;
            var $el = self.$element;
            var navPanelListMarginLeft = Math.abs(parseInt($el.navPanelList.css("margin-left")));
            var navPanelWidth = $el.navPanel.outerWidth(true);
            var sumTabsWidth = sumDomWidth($el.navPanelList.children('li'));
            var leftWidth = 0;
            var marginLeft = 0;
            var $navTabLi;
            if (sumTabsWidth < navPanelWidth) {
                return self
            }
            else {
                $navTabLi = $el.navPanelList.children('li:first');

                while ((marginLeft + $navTabLi.width()) <= navPanelListMarginLeft) {
                    marginLeft += $navTabLi.outerWidth(true);
                    $navTabLi = $navTabLi.next();
                }

                marginLeft = 0;
                if (sumDomWidth($navTabLi.prevAll()) > navPanelWidth) {

                    while (((marginLeft + $navTabLi.width()) < navPanelWidth) && $navTabLi.length > 0) {
                        marginLeft += $navTabLi.outerWidth(true);
                        $navTabLi = $navTabLi.prev();
                    }

                    leftWidth = sumDomWidth($navTabLi.prevAll());
                }
            }
            $el.navPanelList.animate({
                marginLeft: 0 - leftWidth + "px"
            }, "fast");
            return self;
        },

        /**
         * move right
         * @return self
         */
        moveRight: function () {
            var self = this;
            var $el = self.$element;
            var navPanelListMarginLeft = Math.abs(parseInt($el.navPanelList.css("margin-left")));
            var navPanelWidth = $el.navPanel.outerWidth(true);
            var sumTabsWidth = sumDomWidth($el.navPanelList.children('li'));
            var leftWidth = 0;
            var $navTabLi, marginLeft;

            if (sumTabsWidth < navPanelWidth) {
                return self;
            }
            else {
                $navTabLi = $el.navPanelList.children('li:first');
                marginLeft = 0;
                while ((marginLeft + $navTabLi.width()) <= navPanelListMarginLeft) {
                    marginLeft += $navTabLi.outerWidth(true);
                    $navTabLi = $navTabLi.next();
                }
                marginLeft = 0;
                while (((marginLeft + $navTabLi.width()) < navPanelWidth) && $navTabLi.length > 0) {
                    marginLeft += $navTabLi.outerWidth(true);
                    $navTabLi = $navTabLi.next();
                }
                leftWidth = sumDomWidth($navTabLi.prevAll());
                if (leftWidth > 0) {
                    $el.navPanelList.animate({
                        marginLeft: 0 - leftWidth + "px"
                    }, "fast");
                }
            }
            return self;
        },

        /**
         * close navTab
         * @param navTab
         * @return self     Chain structure.
         */
        close: function (navTab) {
            var self = this,$tabPane;
            var $navTab = self._getNavTab(navTab),
            $navTabLi = $navTab.parent('li');
            $tabPane = self._getTabPane($navTab);
            //close unsave tab confirm
            if ($navTabLi.length &&
                $tabPane.length &&
                $tabPane.hasClass('unsave') &&
                !self._unsaveConfirm()) {
                return self;
            }
            if ($navTabLi.find('a').hasClass("active")) {
                var $nextLi = $navTabLi.next("li:first"),
                    $prevLi = $navTabLi.prev("li:last");
                if ($nextLi.length) {
                    self.active($nextLi);
                    self.activeMenu($nextLi.find('a'));
                } else if ($prevLi.length) {
                    self.active($prevLi);
                    self.activeMenu($prevLi.find('a'));
                }
            }
            self._delStorage($navTab.attr('data-id')); //remove tab from session storage
            $navTabLi.remove();
            $tabPane.remove();
            $('.mt-tab-content').trigger('removeNode');
            return self;
        },

        /**
         * close others tab
         * @return self     Chain structure.
         */
        closeOthers: function (retainTab) {
            var self = this,$el = self.$element,findTab;
            if (!retainTab) {
                findTab = $el.navPanelList.find('li a:not([data-type="main"],.active)');
            }
            else {
                findTab = $el.navPanelList.find('a:not([data-type="main"])').filter(function (index) {
                    if (retainTab != $(this).data('index')) return this;
                });
            }

            findTab.each(function () {
                var $navTab = $(this);
                self._delStorage($navTab.attr('data-id')); //remove tab from session storage
                self._getTabPane($navTab).remove(); //remove tab-content
                $navTab.parent('li').remove(); //remove navtab
            });
            if (retainTab) {
                self.active($el.navPanelList.find('a[data-index="' + retainTab + '"]'));
                self.activeMenu($el.navPanelList.find('a[data-index="' + retainTab + '"]'));
            }
            $el.navPanelList.css("margin-left", "0");
            $('.mt-tab-content').trigger('removeNode');
            return self;
        },

        /**
         * focus actived tab
         * @return self     Chain structure.
         */
        showActive: function () {
            var self = this,$el = self.$element;
            var navTab = $el.navPanelList.find('li a.active');
            self._fixTabPosition(navTab);
            return self;
        },

        /**
         * close all tabs, (except main tab)
         * @return self     Chain structure.
         */
        closeAll: function () {
            var self = this,$el = self.$element;
            $el.navPanelList.find('a:not([data-type="main"])').each(function () {
                var $navTab = $(this);
                self._delStorage($navTab.attr('data-id')); //remove tab from session storage
                self._getTabPane($navTab).remove(); //remove tab-content
                $navTab.parent('li').remove(); //remove navtab
            });
            self.active($el.navPanelList.find('a[data-type="main"]:first').parent('li'));
            self.activeMenu($el.navPanelList.find('a[data-type="main"]:first'));
            $('.mt-tab-content').trigger('removeNode');
            $('.dropdown-menu').children().each(function () {
                $(this).removeClass('active');
            });
            return self;
        },

        /**
         * 左侧导航变化
         */
        activeMenu: function (navTab) {
            // 点击选项卡时，左侧菜单栏跟随变化
            var $navObj = $("a[href$='" + $(navTab).data('url') + "']"),   // 当前url对应的左侧导航对象
                $navHasSubnav = $navObj.parents('.nav-item'),
                $viSubHeight = $navHasSubnav.siblings().find('.nav-subnav:visible').outerHeight();

            $('.nav-drawer .nav-item').not($navHasSubnav).each(function () {
                if (window.innerWidth > 1024 && $('body').hasClass('lyear-layout-sidebar-close')) {
                    $(this).find('.nav-subnav').hide();
                }
                $(this).find('.nav-subnav:visible').slideUp(500);
                $(this).removeClass('active open');
            });

            $('.nav-drawer').find('li').removeClass('active');
            $navObj.parent('li').addClass('active');
            $navHasSubnav.addClass('active');

            // 当前菜单无子菜单
            if (!$navObj.parents('.nav-item').first().is('.nav-item-has-subnav')) {
                var hht = 48 * ($navObj.parents('.nav-item').first().prevAll().length - 1);
                $('.lyear-layout-sidebar-info').animate({ scrollTop: hht }, 300);
            }

            var $sideDownObj = $navObj.parents('ul.nav-subnav').filter(':hidden');
            var $sideDownLen = $sideDownObj.length;

            $sideDownObj.each(function (i) {
                $(this).slideDown(500, function () {
                    $(this).parent('.nav-item').addClass('open');

                    if (i === $sideDownLen - 1) {
                        var scrollHeight = 0,
                            $scrollBox = $('.lyear-layout-sidebar-info'),
                            pervTotal = $navHasSubnav.last().prevAll().length,
                            boxHeight = $scrollBox.outerHeight(),
                            innerHeight = $('.sidebar-main').outerHeight(),
                            thisScroll = $scrollBox.scrollTop(),
                            thisSubHeight = $(this).outerHeight(),
                            footHeight = 121;

                        if (footHeight + innerHeight - boxHeight >= (pervTotal * 48)) {
                            scrollHeight = pervTotal * 48;
                        }

                        if ($navHasSubnav.length == 1) {
                            $scrollBox.animate({ scrollTop: scrollHeight }, 300);
                        } else {
                            // 子菜单操作
                            if (typeof ($viSubHeight) != 'undefined' && $viSubHeight != null) {
                                scrollHeight = thisScroll + thisSubHeight - $viSubHeight;
                                $scrollBox.animate({ scrollTop: scrollHeight }, 300);
                            } else {
                                if ((thisScroll + boxHeight - $scrollBox[0].scrollHeight) == 0) {
                                    scrollHeight = thisScroll - thisSubHeight;
                                    $scrollBox.animate({ scrollTop: scrollHeight }, 300);
                                }
                            }
                        }
                    }
                });
            });
        },

        /**
         * 初始化 函数
         * @param options
         * @returns self
         * @private
         */
        _init: function (options) {
            var self = this, $el = self.$element;
            $el.html(defaultLayoutTemplates[options.nav.layout]
                .replace('{mainClass}', options.class)
                .replace('{navClass}', options.nav.class)
                .replace(/\{nav-tabs\}/g, options.nav.style)
                .replace(/\{backgroundColor\}/g, options.nav.backgroundColor)
                .replace('{dropdown}', options.language.nav.dropdown)
                .replace('{showActivedTab}', options.language.nav.showActivedTab)
                .replace('{closeAllTabs}', options.language.nav.closeAllTabs)
                .replace('{closeOtherTabs}', options.language.nav.closeOtherTabs)
            );
            $el.wrapper = $el.find('.mt-wrapper:first');
            $el.nav = $el.find('.mt-nav-bar:first');
            $el.navToolsLeft = $el.nav.find('.mt-nav-tools-left:first');
            $el.navPanel = $el.nav.find('.mt-nav-panel:first');
            $el.navPanelList = $el.nav.find('.mt-nav-panel:first ul');
            //$el.navTabMain    = $('#multitabs_main_0');
            $el.navToolsRight = $el.nav.find('.mt-nav-tools-right:first');
            $el.tabContent = $el.find('.tab-content:first');
            if (options.nav.showTabs == false) {
                $el.nav.hide();
            }
            //set the nav-panel width
            //var toolWidth = $el.nav.find('.mt-nav-tools-left:visible:first').width() + $el.nav.find('.mt-nav-tools-right:visible:first').width();
            $el.navPanel.css('width', 'calc(100% - 147px)');
            self.options = options;
            return self;
        },

        /**
         * 初始化 Multitabs 之后的 最终函数
         * @returns self
         * @private
         */
        _final: function () {
            var self = this, $el = self.$element,options = self.options,storage, init = options.init, param,tempParam;
            if (supportStorage(options.cache)) {
                storage = self._storage();
                self._resetStorage({});
                $.each(storage, function (k, v) {
                    self.create(v, false);
                    if (v.active) {
                        tempParam = self._getParam(v);
                        self.activeMenu(self._exist(tempParam));
                    }
                })
            }
            if ($.isEmptyObject(storage)) {
                init = (!$.isEmptyObject(init) && init instanceof Array) ? init : defaultInit;
                for (var i = 0; i < init.length; i++) {
                    param = self._getParam(init[i]);
                    if (param) {
                        self.create(param);
                    }
                }
            }
            //如果没有一个 tab选择卡选中没那么就会选中main选择卡
            if (!$el.navPanelList.children('li').find('a.active').length) {
                self.active($el.navPanelList.find('[data-type="main"]:first'));
            }
            return self;
        },
        /**
         * 绑定 action
         * @return self
         * @private
         */
        _listen: function () {
            var self = this,$el = self.$element,options = self.options;
            //创建 tab
            handler($(document), 'click', options.selector, function () {
                self.create(this, true);
                if (!$(this).parent().parent('ul').hasClass('dropdown-menu')) {  //下拉菜单中的网址采用data-url，并且不阻止后面的动作
                    return false; //Prevent the default selector action
                }
            });
            //选中的 tab
            handler($el.nav, 'click', '.mt-nav-tab', function () {
                self.active(this, false);
                self.activeMenu(this);
            });

            //拖动 tab
            if (options.nav.draggable) {
                handler($el.navPanelList, 'mousedown', '.mt-nav-tab', function (event) {
                    var $navTab = $(this),$navTabLi = $navTab.closest('li');
                    var $prevNavTabLi = $navTabLi.prev();
                    var dragMode = true, moved = false,isMain = ($navTab.data('type') === "main");
                    var tmpId = 'mt_tmp_id_' + new Date().getTime();
                    var navTabBlankHtml = '<li id="' + tmpId + '" class="mt-dragging" style="width:' + $navTabLi.outerWidth() + 'px; height:' + $navTabLi.outerHeight() + 'px;"><a style="width: 100%;  height: 100%; "></a></li>';
                    var abs_x = event.pageX - $navTabLi.offset().left + $el.nav.offset().left;
                    $navTabLi.before(navTabBlankHtml);
                    $navTabLi.addClass('mt-dragging mt-dragging-tab').css({
                        'left': event.pageX - abs_x + 'px'
                    });

                    $(document).on('mousemove', function (event) {
                        if (dragMode && !isMain) {
                            $navTabLi.css({
                                'left': event.pageX - abs_x + 'px'
                            });
                            $el.navPanelList.children('li:not(".mt-dragging")').each(function () {
                                var leftWidth = $(this).offset().left + $(this).outerWidth() + 20; //间隙增加20像素
                                if (leftWidth > $navTabLi.offset().left) {
                                    if ($(this).next().attr('id') !== tmpId) {
                                        moved = true;
                                        $prevNavTabLi = $(this);
                                        $('#' + tmpId).remove();
                                        $prevNavTabLi.after(navTabBlankHtml);
                                    }
                                    return false;
                                }
                            });
                        }
                    }).on("selectstart", function () { //禁用文本选择
                        if (dragMode) {
                            return false;
                        }
                    }).on('mouseup', function () {
                        if (dragMode) {
                            $navTabLi.removeClass('mt-dragging mt-dragging-tab').css({ 'left': 'auto' });
                            if (moved) {
                                $prevNavTabLi.after($navTabLi);
                            }
                            $('#' + tmpId).remove();
                        }
                        dragMode = false;
                    });
                });
            }

            // 右键菜单
            handler($el.nav, 'contextmenu', '.mt-nav-tab', function (event) {
                event.preventDefault();
                var menu = $('<ul class="dropdown-menu" role="menu" id="contextify-menu"/>');
                var $this = $(this);
                var $nav = $this.closest('li');
                var $navTab = self._getNavTab($nav);
                var $tabPane = self._getTabPane($navTab);
                var  param = $navTab.length ? self._getParam($navTab) : {};

                var menuData = [
                    {
                        text: '刷新', onclick: function () {
                            var tempTabPane = $($tabPane);

                            if (tempTabPane.is('iframe')) {
                                var activeTab = $el.navPanelList.find('li a.active');
                                if (activeTab.data('id') == param.did) {
                                    $('#lyear-loading').fadeIn('fast', function () {
                                        tempTabPane.attr('src', param.url);
                                    });
                                } else {
                                    tempTabPane.attr('src', param.url);
                                }
                            } else {
                                $.ajax({
                                    url: param.url,
                                    dataType: "html",
                                    success: function (callback) {
                                        tempTabPane.html(self.options.content.ajax.success(callback));
                                    },
                                    error: function (callback) {
                                        tempTabPane.html(self.options.content.ajax.error(callback));
                                    }
                                });
                            }
                            menu.hide();

                            return false;
                        }
                    }
                ];

                var param = self._getParam($navTab);
                if (param.type !== 'main') {
                    menuData.push(
                        {
                            text: '关闭', onclick: function () {
                                self.close($navTab);
                                menu.hide();
                                return false;
                            }
                        }
                    );
                }

                menuData.push(
                    {
                        text: '关闭其他', onclick: function () {
                            self.closeOthers($navTab.data('index'));
                            menu.hide();
                            return false;
                        }
                    }
                );

                var l = menuData.length, i;

                for (i = 0; i < l; i++) {
                    var item = menuData[i],el = $('<li/>');

                    el.append('<a class="dropdown-item" />');

                    var a = el.find('a');

                    a.on('click', item.onclick);
                    a.css('cursor', 'pointer');
                    a.html(item.text);

                    menu.append(el);
                }

                var currentMenu = $("#contextify-menu");
                if (currentMenu.length > 0) {
                    if (currentMenu !== menu) {
                        currentMenu.replaceWith(menu);
                    }
                } else {
                    $('body').append(menu);
                }

                var clientTop = $(window).scrollTop() + event.clientY;
                var x = (menu.width() + event.clientX < $(window).width()) ? event.clientX : event.clientX - menu.width();
                var y = (menu.height() + event.clientY < $(window).height()) ? clientTop : clientTop - menu.height();

                menu.css('top', y).css('left', x).css('position', 'fixed').show();


                $(this).parents().on('click', function () {
                    menu.hide();
                });
                $('#iframe-content').find('iframe').contents().find('body').on('click', function () {
                    menu.hide();
                });
            });

            // 双击事件
            handler($el.nav, 'dblclick', '.mt-nav-tab', function (event) {
                if (options.dbclickRefresh === true) {
                    var $this = $(this);
                    var $nav = $this.closest('li');
                    var $navTab = self._getNavTab($nav);
                    var $tabPane = self._getTabPane($navTab);
                    var param = $navTab.length ? self._getParam($navTab) : {};
                    var  tempTabPane = $($tabPane);

                    if (tempTabPane.is('iframe')) {
                        $('#lyear-loading').fadeIn('fast', function () {
                            tempTabPane.attr('src', param.url);
                        });
                    } else {
                        $.ajax({
                            url: param.url,
                            dataType: "html",
                            success: function (callback) {
                                tempTabPane.html(self.options.content.ajax.success(callback));
                            },
                            error: function (callback) {
                                tempTabPane.html(self.options.content.ajax.error(callback));
                            }
                        });
                    }
                }

                return false;
            });

            //close tab
            handler($el.nav, 'click', '.mt-close-tab', function () {
                self.close($(this).closest('li'));
                var url = $(this).prev().attr("data-url");
                $('[data-href="' + url + '"]').parent().removeClass('active');
                return false; //避免可能的BUG
            });
            //move left
            handler($el.nav, 'click', '.mt-move-left', function () {
                self.moveLeft();
                return false; //避免可能的BUG
            });
            //move right
            handler($el.nav, 'click', '.mt-move-right', function () {
                self.moveRight();
                return false; //避免可能的BUG
            });
            //show actived tab
            handler($el.nav, 'click', '.mt-show-actived-tab', function () {
                self.showActive();
                //return false; //避免可能的BUG
            });
            //close all tabs
            handler($el.nav, 'click', '.mt-close-all-tabs', function () {
                self.closeAll();
                //return false; //避免可能的BUG
            });
            //close other tabs
            handler($el.nav, 'click', '.mt-close-other-tabs', function () {
                var dataUrl = $(".nav-link.mt-nav-tab.active").data("url");
                $(".nav-link.multitabs").each(function () {
                    var parent = $(this).parent();
                    var url = $(this).attr('data-href');
                    if (parent.hasClass("active") && dataUrl != url) {
                        // 在这里编写处理父级元素有 "active" class 的代码
                        parent.removeClass('active');
                    }
                });
                self.closeOthers();
                //return false; //避免可能的BUG
            });

            //固定导航条
            var navHeight = $el.nav.outerHeight();
            $el.tabContent.css('paddingTop', navHeight);
            if (options.nav.fixed) {
                handler($(window), 'scroll', function () {
                    var scrollTop = $(this).scrollTop();
                    scrollTop = scrollTop < ($el.wrapper.height() - navHeight) ? scrollTop + 'px' : 'auto';
                    $el.nav.css('top', scrollTop);
                    return false; //避免可能的BUG
                });
            }
            //if layout === 'classic' show hide list in dropdown menu
            if (options.nav.layout === 'classic') {
                handler($el.nav, 'click', '.mt-dropdown:not(.open)', function () { //just trigger when dropdown not open.
                    var list = self._getHiddenList();
                    var $dropDown = $el.navToolsRight.find('.mt-hidden-list:first').empty();
                    if (list) { //when list is not empty
                        while (list.prevList.length) {
                            $dropDown.append(list.prevList.shift().clone());
                        }
                        while (list.nextList.length) {
                            $dropDown.append(list.nextList.shift().clone());
                        }
                    } else {
                        $dropDown.append('<li>empty</li>');
                    }
                });
            }
            return self;
        },

        /**
         * 获取multitabs 对象的参数
         * @param obj          multitabs 对象
         * @returns param      param
         * @private
         */
        _getParam: function (obj) {
            if ($.isEmptyObject(obj)) {
                return false;
            }
            var self = this;
            var options = self.options;
            var param = {};
            var $obj = $(obj);
            var  data = $obj.data();

            //content
            param.content = data.content || obj.content || '';

            if (!param.content.length) {
                //url
                param.url = data.url || obj.url || $obj.attr('data-href') || $obj.attr('url') || '';
                param.url = $.trim(decodeURIComponent(param.url.replace('#', '')));
            } else {
                param.url = '';
            }
            if (!param.url.length && !param.content.length) {
                return false;
            }
            //isNewTab
            param.isNewTab = data.hasOwnProperty('isNewTab') || obj.hasOwnProperty('isNewTab') || options.isNewTab;
            //iframe
            param.iframe = data.iframe || obj.iframe || isExtUrl(param.url) || options.iframe;
            //type
            param.type = data.type || obj.type || options.type;
            //title
            param.title = data.title || obj.title || $obj.text() || param.url.replace('http://', '').replace('https://', '') || options.language.nav.title;
            param.title = trimText(param.title, options.nav.maxTitleLength);
            //active
            param.active = data.active || obj.active || false;
            //index
            param.index = data.index || obj.index || getTabIndex(param.type, param.url);
            //id
            param.did = data.did || obj.did || this._generateId(param);
            return param;
        },

        /**
         * sessionStorage 存储 tab 数组
         * @param key
         * @param param
         * @private
         */
        _storage: function (key, param) {
            if (supportStorage(this.options.cache)) {
                var storage = JSON.parse(sessionStorage.multitabs || '{}');
                if (!key) {
                    return storage;
                }
                if (!param) {
                    return storage[key];
                }
                storage[key] = param;
                sessionStorage.multitabs = JSON.stringify(storage);
                return storage;
            }
            return {};
        },

        /**
         * 通过key删除 sessionStorage存储的数据
         * @param key
         * @private
         */
        _delStorage: function (key) {
            if (supportStorage(this.options.cache)) {
                var storage = JSON.parse(sessionStorage.multitabs || '{}');
                if (!key) {
                    return storage;
                }
                delete storage[key];
                sessionStorage.multitabs = JSON.stringify(storage);
                return storage;
            }
            return {};
        },

        /**
         * 重置 sessionStorage
         * @param storage
         * @private
         */
        _resetStorage: function (storage) {
            if (supportStorage(this.options.cache) && typeof storage === "object") {
                sessionStorage.multitabs = JSON.stringify(storage);
            }
        },

        /**
         * 检查是否存在multitabs对象
         * @param param
         * @private
         */
        _exist: function (param) {
            if (!param || !param.url) {
                return false;
            }
            var self = this;
            var $el = self.$element;
            var $navTab = $el.navPanelList.find('a[data-url="' + param.url + '"]:first');
            if ($navTab.length) {
                return $navTab;
            } else {
                return false;
            }
        },

        /**
         * get tab-pane from tab
         * @param tab
         * @returns {*}
         * @private
         */
        _getTabPane: function (navTab) {
            var result = $('#' + $(navTab).attr('data-id'));
            return result;
        },

        /**
         * get real navTab in the panel list.
         * @param navTab
         * @returns navTab
         * @private
         */
        _getNavTab: function (navTab) {
            var self = this;
            var $el = self.$element;
            var dataId = $(navTab).attr('data-id') || $(navTab).find('a').attr('data-id');
            var result = $el.navPanelList.find('a[data-id="' + dataId + '"]:first');
            return result;
        },

        /**
         * fix nav navTab position
         * @param navTab
         * @private
         */
        _fixTabPosition: function (navTab) {
            var self = this;
            var $el = self.$element;
            var $navTabLi = $(navTab).parent('li');
            var tabWidth = $navTabLi.outerWidth(true);
            var prevWidth = $navTabLi.prev().outerWidth(true);
            var pprevWidth = $navTabLi.prev().prev().outerWidth(true);
            var sumPrevWidth = sumDomWidth($navTabLi.prevAll());
            var sumNextWidth = sumDomWidth($navTabLi.nextAll());
            var navPanelWidth = $el.navPanel.outerWidth(true);
            var sumTabsWidth = sumDomWidth($el.navPanelList.children('li'));
            var leftWidth = 0;
            //all nav navTab's width no more than nav-panel's width
            if (sumTabsWidth < navPanelWidth) {
                leftWidth = 0
            } else {
                //when navTab and his right tabs sum width less or same as nav-panel, it means nav-panel can contain the navTab and his right tabs
                if ((prevWidth + tabWidth + sumNextWidth) <= navPanelWidth) {
                    leftWidth = sumPrevWidth; //sum width of left part
                    //add width from the left, calcular the maximum tabs can contained by nav-panel
                    while ((sumTabsWidth - leftWidth + prevWidth) < navPanelWidth) {
                        $navTabLi = $navTabLi.prev(); //change the left navTab
                        leftWidth -= $navTabLi.outerWidth(); //reduce the left part width
                    }
                } else { //nav-panel can not contain the navTab and his right tabs
                    //when the navTab and his left part tabs's sum width more than nav-panel, all the width of 2 previous tabs's width set as the nav-panel margin-left.
                    if ((sumPrevWidth + tabWidth) > navPanelWidth) {
                        leftWidth = sumPrevWidth - prevWidth - pprevWidth
                    }
                }
            }
            leftWidth = leftWidth > 0 ? leftWidth : 0; //avoid leftWidth < 0 BUG
            $el.navPanelList.animate({
                marginLeft: 0 - leftWidth + "px"
            }, "fast");
        },

        /**
         * hidden tab list
         * @returns hidden tab list, the prevList and nextList
         * @private
         */
        _getHiddenList: function () {
            var self = this;
            var $el = self.$element;
            var navPanelListMarginLeft = Math.abs(parseInt($el.navPanelList.css("margin-left")));
            var navPanelWidth = $el.navPanel.outerWidth(true);
            var sumTabsWidth = sumDomWidth($el.navPanelList.children('li'));
            var tabPrevList = [];
            var tabNextList = [];
            var $navTabLi;
            var  marginLeft;
            //all tab's width no more than nav-panel's width
            if (sumTabsWidth < navPanelWidth) {
                return false;
            } else {
                $navTabLi = $el.navPanelList.children('li:first');
                //overflow hidden left part
                marginLeft = 0;
                //from the first tab, add all left part hidden tabs
                while ((marginLeft + $navTabLi.width()) <= navPanelListMarginLeft) {
                    marginLeft += $navTabLi.outerWidth(true);
                    tabPrevList.push($navTabLi);
                    $navTabLi = $navTabLi.next();
                }
                //overflow hidden right part
                if (sumTabsWidth > marginLeft) { //check if the right part have hidden tabs
                    $navTabLi = $el.navPanelList.children('li:last');
                    marginLeft = sumTabsWidth; //set margin-left as the Rightmost, and reduce one and one.
                    while (marginLeft > (navPanelListMarginLeft + navPanelWidth)) {
                        marginLeft -= $navTabLi.outerWidth(true);
                        tabNextList.unshift($navTabLi); //add param from top
                        $navTabLi = $navTabLi.prev();
                    }
                }
                return {
                    prevList: tabPrevList,
                    nextList: tabNextList
                };
            }
        },



        /**
         * check if tab-pane is iframe, and add/remove class
         * @param tabPane
         * @private
         */
        _fixTabContentLayout: function (tabPane) {
            var $tabPane = $(tabPane);
            if ($tabPane.is('iframe')) {
                $('body').addClass('full-height-layout');
                /** fix chrome croll disappear bug **/
                $tabPane.css("height", "99%");
                window.setTimeout(function () {
                    $tabPane.css("height", "100%");
                }, 0);
            } else {
                $('body').removeClass('full-height-layout');
            }
        },
    };

    /**
     * 入口 function
     * @param option
     */
    $.fn.multitabs = function (option, id) {
        var self = $(this);
        var did = id ? id : 'multitabs';
        var multitabs = $(document).data(did);
        var options = typeof option === 'object' && option;
        var  opts;
        if (!multitabs) {
            opts = $.extend(true, {}, $.fn.multitabs.defaults, options, self.data());
            opts.nav.style = (opts.nav.style === 'nav-pills') ? 'nav-pills' : 'nav-tabs';
            multitabs = new MultiTabs(this, opts);
            $(document).data(did, multitabs);
        }
        var result = $(document).data(did);
        return result;
    };

    /**
     * 默认 Options
     * @type {}
     */
    $.fn.multitabs.defaults = {
        selector: '.multitabs', // selector text to trigger multitabs.
        iframe: true, // Global iframe mode, default is false, is the auto mode (for the self page, use ajax, and the external, use iframe)
        cache: true, // 是否缓存当前打开的tab
        class: '', // class for whole multitabs
        type: 'info', // change the info content name, is not necessary to change.
        init: [],
        isNewTab: false, // 是否以新tab标签打开，为true时，每次点击都会打开新的tab
        refresh: 'no', // iframe中页面是否刷新，'no'：'从不刷新'，'nav'：'点击菜单刷新'，'all'：'菜单和tab点击都刷新'
        dbclickRefresh: false, // 双击刷新开启最好不要和refresh:'all'同时使用
        nav: {
            backgroundColor: '#f5f5f5', //default nav-bar background color
            class: '', //class of nav
            draggable: true, //nav tab draggable option
            fixed: false, //fixed the nav-bar
            layout: 'default', //it can be 'default', 'classic' (all hidden tab in dropdown list), and simple
            showTabs: true, // 是否显示Tab标签栏
            maxTitleLength: 25, //Max title length of tab
            showCloseOnHover: false, //while is true, show close button in hover, if false, show close button always
            style: 'nav-tabs' //can be nav-tabs or nav-pills
        },
        content: {
            ajax: {
                class: '', //Class for ajax tab-pane
                error: function (htmlCallBack) {
                    //modify html and return
                    return htmlCallBack;
                },
                success: function (htmlCallBack) {
                    //modify html and return
                    return htmlCallBack;
                }
            },
            iframe: {
                class: ''
            }
        },
        language: { //language setting
            nav: {
                title: 'Tab', //default tab's tittle
                dropdown: '<i class="mdi mdi-menu"></i>', //right tools dropdown name
                showActivedTab: '显示当前选项卡', //show active tab
                closeAllTabs: '关闭所有标签页', //close all tabs
                closeOtherTabs: '关闭其他标签页', //close other tabs
            }
        }
    };
})(jQuery));