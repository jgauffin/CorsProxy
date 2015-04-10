/* Released under Apache 2.0 License
 * (c) Jonas Gauffin
 *
 * Sponsored by https://onetrueerror.com
 */

var getIeVersion = function () {
    if (navigator.appName == "Microsoft Internet Explorer") {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[.0-9]{0,})");
        if (re.exec(ua) != null) {
            return parseInt(RegExp.$1);
        }
    } else {
        return false;
    }
};

if ($.ajaxPrefilter) {
    $.ajaxPrefilter(function (options, originalOptions, jqXhr) {
        if (!window.CorsProxyUrl) {
            window.CorsProxyUrl = '/corsproxy/';
        }
        // only proxy those requests
        // that are marked as crossDomain requests.
        if (!options.crossDomain) {
            return;
        }

        //true in this script so that it's always called in this demo
        if (getIeVersion() && getIeVersion() < 10 || true) {
            var url = options.url;
            var questionPos = url.indexOf('?');
            if (questionPos == -1) {
                url += '?' + options.data;
            } else {
                url += '&' + options.data;
            }
            options.beforeSend = function (request) {
                request.setRequestHeader("X-CorsProxy-Url", url);
            };
            options.url = window.CorsProxyUrl;
            options.crossDomain = false;
        }
    });
} else {
    $.ajaxSetup({
        beforeSend: function () {
            var options = this;
            if (!window.CorsProxyUrl) {
                window.CorsProxyUrl = '/corsproxy/';
            }
            if (!options.crossDomain) {
                return;
            }
            if (getIeVersion() && getIeVersion() < 10) {
                var url = options.url;
                var questionPos = url.indexOf('?');
                if (questionPos == -1) {
                    url += '?' + options.data;
                } else {
                    url += '&' + options.data;
                }
                options.beforeSend = function (request) {
                    request.setRequestHeader("X-CorsProxy-Url", url);
                };
                options.url = window.CorsProxyUrl;
                options.crossDomain = false;
            }
        }
    });
}
