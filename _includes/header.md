<nav class="navbar navbar-default navbar-fixed-top bs-docs-nav" role="navigation">
    <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">
                   {{ site.title }}
                </a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li>
                        <a href="/">
                            首页
                        </a>
                    </li>
					<li>
                        <a href="/docs/changelog">
                            更新日志
                        </a>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">帮助文档<span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
						    <li><a href="/docs/info">程序简介</a></li>
                            <li><a href="/docs/help/install">安装说明</a></li>
							<li><a href="/docs/help/setting">基本设置</a></li>
							<li><a href="/docs/help/advanced">高级技能</a></li>
                            <li><a href="/docs/help/plugin">插件使用</a></li>
                            <li class="divider"></li>
                            <li><a href="/docs/help/faq">常见问题</a></li>
                            <li class="divider"></li>
                            <li><a href="/docs/license">License</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="/docs/develope">
                            开发者文档
                        </a>
                    </li>
					
                </ul>
                <div class="hidden-sm hidden-xs nav-lang pull-right">
                    <div class="btn-group">
                        <button type="button" class="btn btn-xs btn-default btn-md dropdown-toggle"
                        data-toggle="dropdown">
                            当前语言:简体中文
                        </button>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="javascript::" data-lang="en-US" class="lang-changed">
                                    English
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="hidden-sm hidden-xs nav-github pull-right">
                    <span rel="show-github" data-user={{ site.soft.author.github-user }} data-repo={{ site.soft.author.github-repo }} data-type="watch">
                    </span>
                    <span rel="show-github" data-user={{ site.soft.author.github-user }} data-repo={{ site.soft.author.github-repo }} data-type="fork">
                    </span>
                </div>
            </div>
    </div>
</nav>