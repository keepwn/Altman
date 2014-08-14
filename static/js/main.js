(function($){

	// Avoid embed thie site in an iframe of other WebSite
	if (top.location != location) {
		top.location.href = location.href;
	}

	// btn checked box toggle
	$(document).on('click', '.btn-checked', function(){
		var $e = $(this);
		var $i = $e.siblings('[name='+$e.data('name')+']');
		if($e.hasClass('active')) {
			$i.val('true');
		} else {
			$i.val('false');
		}
		$e.blur();
	});

	// change locale and reload page
	$(document).on('click', '.lang-changed', function(){
		var $e = $(this);
		var lang = $e.data('lang');
		$.cookie('lang', lang, {path: '/', expires: 365});
		window.location.reload();
	});

	(function(){
		$.fn.mdFilter = function(){
			var $e = $(this);
			$e.find('img').each(function(_,img){
				var $img = $(img);
				$img.addClass('img-responsive');
			});

			var $pre = $e.find('pre > code').parent();
			$pre.addClass("prettyprint");
			prettyPrint();
		};

	})();

	(function(){
		var caches = {};
		$.fn.showGithub = function(user, repo, type, count){

			$(this).each(function(){
				var $e = $(this);

				var user = $e.data('user') || user,
				repo = $e.data('repo') || repo,
				type = $e.data('type') || type || 'watch',
				count = $e.data('count') == 'true' || count || true;

				var $mainButton = $e.html('<span class="github-btn"><a class="btn btn-xs btn-default" href="#" target="_blank"><i class="icon-github"></i> <span class="gh-text"></span></a><a class="gh-count"href="#" target="_blank"></a></span>').find('.github-btn'),
				$button = $mainButton.find('.btn'),
				$text = $mainButton.find('.gh-text'),
				$counter = $mainButton.find('.gh-count');

				function addCommas(a) {
					return String(a).replace(/(\d)(?=(\d{3})+$)/g, '$1,');
				}

				function callback(a) {
					if (type == 'watch') {
						$counter.html(addCommas(a.watchers));
					} else {
						if (type == 'fork') {
							$counter.html(addCommas(a.forks));
						} else {
							if (type == 'follow') {
								$counter.html(addCommas(a.followers));
							}
						}
					}

					if (count) {
						$counter.css('display', 'inline-block');
					}
				}

				function jsonp(url) {
					var ctx = caches[url] || {};
					caches[url] = ctx;
					if(ctx.onload || ctx.data){
						if(ctx.data){
							callback(ctx.data);
						} else {
							setTimeout(jsonp, 500, url);
						}
					}else{
						ctx.onload = true;
						$.getJSON(url, function(a){
							ctx.onload = false;
							ctx.data = a;
							callback(a);
						});
					}
				}

				var urlBase = 'https://github.com/' + user + '/' + repo;
				
				$button.attr('href', urlBase + '/');

				if (type == 'watch') {
					$mainButton.addClass('github-watchers');
					$text.html('Star');
					$counter.attr('href', urlBase + '/stargazers');
				} else {
					if (type == 'fork') {
						$mainButton.addClass('github-forks');
						$text.html('Fork');
						$counter.attr('href', urlBase + '/network');
					} else {
						if (type == 'follow') {
							$mainButton.addClass('github-me');
							$text.html('Follow @' + user);
							$button.attr('href', 'https://github.com/' + user);
							$counter.attr('href', 'https://github.com/' + user + '/followers');
						}
					}
				}

				if (type == 'follow') {
					jsonp('https://api.github.com/users/' + user);
				} else {
					jsonp('https://api.github.com/repos/' + user + '/' + repo);
				}

			});
		};

	})();


	$(function(){
        // Encode url.
        var $doc = $('.docs-markdown');
        $doc.find('a').each(function () {
            var node = $(this);
            var link = node.attr('href');
            var index = link.indexOf('#');
            if (link.indexOf('http') === 0 && link.indexOf(window.location.hostname) == -1) {
                return;
            }
            if (index < 0 || index + 1 > link.length) {
                return;
            }
            var val = link.substring(index + 1, link.length);
            val = ((val).replace(/[^-\w\u4e00-\u9fa5]/g, "-"));
            node.attr('href', link.substring(0, index) + '#' + val);
        });

        // Set anchor.
        $doc.find('h1, h2, h3, h4, h5, h6').each(function () {
            var node = $(this);
            if (node.hasClass('ui')) {
                return;
            }
            var val = (node.text().replace(/[^-\w\u4e00-\u9fa5]/g, "-"));
            //node = node.wrap('<div id="' + val + '" class="anchor-wrap" ></div>');
            node.append('<a class="anchor" href="#' + val + '"><span class="octicon octicon-link"></span></a>');
        });
    });

	$(function(){
		// on dom ready

		$('[data-show=tooltip]').each(function(k, e){
			var $e = $(e);
			$e.tooltip({placement: $e.data('placement'), title: $e.data('tooltip-text')});
			$e.tooltip('show');
		});

		$('.markdown').mdFilter();

		$('[rel=show-github]').showGithub();

		if($.jPanelMenu && $('[data-toggle=jpanel-menu]').size() > 0) {
			var jpanelMenuTrigger = $('[data-toggle=jpanel-menu]');

			var jPM = $.jPanelMenu({
				animated: false,
				menu: jpanelMenuTrigger.data('target'),
				direction: 'left',
				trigger: '.'+ jpanelMenuTrigger.attr('class'),
				excludedPanelContent: '.jpanel-menu-exclude',
				openPosition: '180px',
				afterOpen: function() {
					jpanelMenuTrigger.addClass('open');
					$('html').addClass('jpanel-menu-open');
				},
				afterClose: function() {
					jpanelMenuTrigger.removeClass('open');
					$('html').removeClass('jpanel-menu-open');
				}
			});

			//jRespond settings
			var jRes = jRespond([{
				label: 'small',
				enter: 0,
				exit: 1010
			}]);

			//turn jPanel Menu on/off as needed
			jRes.addFunc({
			breakpoint: 'small',
				enter: function() {
					jPM.on();
				},
				exit: function() {
					jPM.off();
				}
			});
		}

	});

})(jQuery);