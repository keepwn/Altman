/*
 * jQuery Table of Content Generator for Markdown v1.0
 *
 * https://github.com/dafi/tocmd-generator
 * Examples and documentation at: https://github.com/dafi/tocmd-generator
 *
 * Requires: jQuery v1.7+
 *
 * Copyright (c) 2013 Davide Ficano
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 */
(function($) {
	
	String.prototype.format = function(args) {
        var result = this;
        if (arguments.length > 0) {    
            if (arguments.length == 1 && typeof (args) == "object") {
                for (var key in args) {
                    if(args[key]!=undefined){
                        var reg = new RegExp("({" + key + "})", "g");
                        result = result.replace(reg, args[key]);
                    }
                }
            }
            else {
                for (var i = 0; i < arguments.length; i++) {
                    if (arguments[i] != undefined) {
　　　　　　　　　　　　var reg= new RegExp("({)" + i + "(})", "g");
                        result = result.replace(reg, arguments[i]);
                    }
                }
            }
        }
        return result;
    }

    var toggleHTML = '<li class="toc-title"><a>{0}</a></li>';
    var tocContainerHTML = '<div class="col-md-3" id="toc-container"><div class="bs-docs-sidebar hidden-print hidden-xs hidden-sm" role="complementary"><ul class="nav bs-docs-sidenav">{0}{1}</ul></div></div>';

    function createLevelHTML(anchorId, tocText, tocInner) {
        var link = '<a href="#{0}">{1}</a>{2}'
            .format(anchorId, tocText, tocInner ? tocInner : '');
        return '<li>{0}</li>\n'
            .format(link);
    }

    $.fn.toc = function(settings) {
        var config = {
            anchorPrefix: '',
            showAlways: false,
            saveShowStatus: true,
            titleText: 'Title',
            hideText: 'hide',
            showText: 'show'};

        if (settings) {
            $.extend(config, settings);
        }

        var tocHTML = '';
        var tocLevel = 1;
        var tocSection = 1;
        var itemNumber = 1;

        var tocContainer = $(this);

		
		var h1 = tocContainer.find('h1:eq(0)');
		var titleText = h1.text();
		
        tocContainer.find('h2').each(function() {
            var levelHTML = '';
            var innerSection = 0;
            var h2 = $(this);

            h2.nextUntil('h2').filter('h3').each(function() {
                ++innerSection;
				var val = $(this).text();
                var anchorId = config.anchorPrefix + (val.replace(/[^-\w\u4e00-\u9fa5]/g, "-"));
                $(this).attr('id', anchorId);
				$(this).attr('class','anchor-wrap');
                levelHTML += createLevelHTML(anchorId,
                    val);
            });
			

			if (levelHTML) {
                levelHTML = '<ul class="nav">' + levelHTML + '</ul>\n';
            }
			var val = h2.text();
            var anchorId = config.anchorPrefix + (val.replace(/[^-\w\u4e00-\u9fa5]/g, "-"));
            h2.attr('id', anchorId);
			h2.attr('class','anchor-wrap');
            tocHTML += createLevelHTML(anchorId,
                val,
				levelHTML);

            tocSection += 1 + innerSection;
            ++itemNumber;
        });

        var hasOnlyOneTocItem = tocLevel == 1 && tocSection <= 2;
        var show = config.showAlways ? true : !hasOnlyOneTocItem;

        // check if cookie plugin is present otherwise doesn't try to save
        if (config.saveShowStatus && typeof($.cookie) == "undefined") {
            config.saveShowStatus = false;
        }

        if (show && tocHTML) {
            var replacedToggleHTML = toggleHTML
                .format(titleText);
            var replacedTocContainer = tocContainerHTML
                .format(replacedToggleHTML, tocHTML);

            tocContainer.append(replacedTocContainer);

            $('#toctogglelink').click(function() {
                var ul = $($('#toc ul')[0]);
                
                if (ul.is(':visible')) {
                    ul.hide();
                    $(this).text(config.showText);
                    if (config.saveShowStatus) {
                        $.cookie('toc-hide', '1', { expires: 365, path: '/' });
                    }
                    $('#toc').addClass('tochidden');
                } else {
                    ul.show();
                    $(this).text(config.hideText);
                    if (config.saveShowStatus) {
                        $.removeCookie('toc-hide', { path: '/' });
                    }
                    $('#toc').removeClass('tochidden');
                }
                return false;
            });

            if (config.saveShowStatus && $.cookie('toc-hide')) {
                var ul = $($('#toc ul')[0]);
                
                ul.hide();
                $('#toctogglelink').text(config.showText);
                $('#toc').addClass('tochidden');
            }
        }
        return this;
    }


	$(function(){
		$('.page-content').toc({
				showAlways:true
		});
		/*
		$('.bs-docs-sidebar').affix({
            offset: {
                top: 50,
				bottom: function () {
                    return (this.bottom = $('.page-footer').outerHeight(true))
				}
            }
        });*/
		
		//$('body').scrollspy()
	})
})(jQuery);


!function(a) {
	"use strict";
	a(function() {
		var b = a(window);
		var c = a(document.body);
		c.scrollspy({
			target: ".bs-docs-sidebar"
		});
		b.on("load", function() {
			c.scrollspy("refresh")
		});
		a(".bs-docs-container [href=#]").click(function(a) {
			a.preventDefault()
		});
		setTimeout(function() {
			var b = a(".bs-docs-sidebar");
			b.affix({
				offset: {
					top: function() {
						var c = b.offset().top,
							d = parseInt(b.children(0).css("margin-top"), 10),
							e = a(".bs-docs-nav").height();
						return this.top = c - e - d
					},
					bottom: function() {
						return this.bottom = a(".page-footer").outerHeight(true)
					}
				}
			})
		}, 100);
	})
}(jQuery);