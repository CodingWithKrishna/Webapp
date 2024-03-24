/**
Native toast
*/
!function (t, e) { "object" == typeof exports && "undefined" != typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define(e) : t.nativeToast = e() }(this, function () { "use strict"; var t = Object.getOwnPropertySymbols, e = Object.prototype.hasOwnProperty, o = Object.prototype.propertyIsEnumerable; var i = function () { try { if (!Object.assign) return !1; var t = new String("abc"); if (t[5] = "de", "5" === Object.getOwnPropertyNames(t)[0]) return !1; for (var e = {}, o = 0; o < 10; o++)e["_" + String.fromCharCode(o)] = o; if ("0123456789" !== Object.getOwnPropertyNames(e).map(function (t) { return e[t] }).join("")) return !1; var i = {}; return "abcdefghijklmnopqrst".split("").forEach(function (t) { i[t] = t }), "abcdefghijklmnopqrst" === Object.keys(Object.assign({}, i)).join("") } catch (t) { return !1 } }() ? Object.assign : function (i, n) { for (var s, r, a = arguments, c = function (t) { if (null == t) throw new TypeError("Object.assign cannot be called with null or undefined"); return Object(t) }(i), h = 1; h < arguments.length; h++) { for (var u in s = Object(a[h])) e.call(s, u) && (c[u] = s[u]); if (t) { r = t(s); for (var d = 0; d < r.length; d++)o.call(s, r[d]) && (c[r[d]] = s[r[d]]) } } return c }, n = null, s = { warning: '<svg viewBox="0 0 32 32" width="32" height="32" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="6.25%"><path d="M8 17 C8 12 9 6 16 6 23 6 24 12 24 17 24 22 27 25 27 25 L5 25 C5 25 8 22 8 17 Z M20 25 C20 25 20 29 16 29 12 29 12 25 12 25 M16 3 L16 6" /></svg>', success: '<svg viewBox="0 0 32 32" width="32" height="32" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="6.25%"><path d="M2 20 L12 28 30 4" /></svg>', info: '<svg viewBox="0 0 32 32" width="32" height="32" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="6.25%"><path d="M16 14 L16 23 M16 8 L16 10" /><circle cx="16" cy="16" r="14" /></svg>', error: '<svg viewBox="0 0 32 32" width="32" height="32" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="6.25%"><path d="M16 3 L30 29 2 29 Z M16 11 L16 19 M16 23 L16 25" /></svg>' }, r = function (t) { void 0 === t && (t = {}); var e = t.message; void 0 === e && (e = ""); var o = t.position; void 0 === o && (o = "bottom"); var i = t.timeout; void 0 === i && (i = 3e3); var r = t.el; void 0 === r && (r = document.body); var a = t.square; void 0 === a && (a = !1); var c = t.type; void 0 === c && (c = ""); var h = t.debug; void 0 === h && (h = !1); var u = t.edge; void 0 === u && (u = !1), n && n.destroy(), this.message = e, this.position = o, this.el = r, this.timeout = i, this.toast = document.createElement("div"), this.toast.className = "native-toast native-toast-" + this.position, c && (this.toast.className += " native-toast-" + c, this.message = '<span class="native-toast-icon-' + c + '">' + (s[c] || "") + "</span>" + this.message), this.toast.innerHTML = this.message, u && (this.toast.className += " native-toast-edge"), a && (this.toast.style.borderRadius = "3px"), this.el.appendChild(this.toast), n = this, this.show(), h || this.hide() }; function a(t) { return new r(t) } r.prototype.show = function () { var t = this; setTimeout(function () { t.toast.classList.add("native-toast-shown") }, 300) }, r.prototype.hide = function () { var t = this; setTimeout(function () { t.destroy() }, this.timeout) }, r.prototype.destroy = function () { var t = this; this.toast && (this.toast.classList.remove("native-toast-shown"), setTimeout(function () { t.toast && (t.el.removeChild(t.toast), t.toast = null) }, 300)) }; for (var c = function () { var t = u[h]; a[t] = function (e) { return a(i({}, { type: t }, e)) } }, h = 0, u = ["success", "info", "warning", "error"]; h < u.length; h += 1)c(); return a });




/*  Header Menu */
var element = document.getElementById("nav-icon2"), mobilemenu = document.getElementById("navbarsExample07"); !function (e) { if ("object" == typeof exports && "undefined" != typeof module) module.exports = e(); else if ("function" == typeof define && define.amd) define([], e); else { ("undefined" != typeof window ? window : "undefined" != typeof global ? global : "undefined" != typeof self ? self : this).Sidenav = e() } }(function () {
	return function e(t, n, i) { function s(o, c) { if (!n[o]) { if (!t[o]) { var a = "function" == typeof require && require; if (!c && a) return a(o, !0); if (r) return r(o, !0); var d = new Error("Cannot find module '" + o + "'"); throw d.code = "MODULE_NOT_FOUND", d } var u = n[o] = { exports: {} }; t[o][0].call(u.exports, function (e) { var n = t[o][1][e]; return s(n || e) }, u, u.exports, e, t, n, i) } return n[o].exports } for (var r = "function" == typeof require && require, o = 0; o < i.length; o++)s(i[o]); return s }({
		1: [function (e, t, n) {
			"use strict"; function i(e) {
				e = e || {}, this.extraClosePixels = e.extraClosePixels || 30, this.width = e.width || 350, this.sidenavOpacity = e.sidenavOpacity || .5, this.isBusy = !1, this.isOpened = !1, this.currentOpacity = 0, this.currentWidth = 0, this.sidenav = e.sidenav, this.backdrop = e.backdrop,
				this.sidenav.classList.add("sn-sidenav"), this.sidenav.style.transform = "translate3d(" + (-1 * this.width - this.extraClosePixels) + "px, 0, 0)", this.backdrop.classList.add("sn-backdrop"), this.initEvents()
			} function s(e, t, n, i) { return -n * (e /= i) * (e - 2) + t } t.exports = i; var r = window.document, o = r.documentElement; i.prototype.open = function () { var e = this; return e.isBusy ? Promise.reject() : (e.isBusy = !0, o.classList.add("sn-visible"), mobilemenu.classList.add("show"), element.classList.add("open"), $("#nav-icon2").addClass("open"), this.showHideSidebarBackdrop(!0).then(function () { e.isBusy = !1, e.isOpened = !0 })) }, i.prototype.close = function () { var e = this; return e.isBusy ? Promise.reject() : (e.isBusy = !0, this.showHideSidebarBackdrop(!1).then(function () { e.isBusy = !1, e.isOpened = !1, o.classList.remove("sn-visible"), element.classList.remove("open") })) }, i.prototype.expandTo = function (e) { var t = this; e = Math.min(e, t.width); var n = t.sidenavOpacity * e / t.width; o.classList.add("sn-visible"), t.sidenav.style.transform = "translate3d(" + (-t.width + e) + "px, 0, 0)", t.backdrop.style.opacity = n, t.currentOpacity = n, t.currentWidth = e }, i.prototype.showHideSidebarBackdrop = function (e) { var t = this; return new Promise(function (n) { var i = 300, r = null; console.log(), requestAnimationFrame(function o(c) { var a = 0; null === r ? r = c : a = Math.min(c - r, i); var d = null, u = null; e ? (d = s(a, t.currentOpacity, t.sidenavOpacity - t.currentOpacity, i), u = s(a, t.currentWidth, t.width - t.currentWidth, i)) : (d = t.currentOpacity - s(a, 0, t.currentOpacity, i), u = t.currentWidth - s(a, 0, t.currentWidth + t.extraClosePixels, i)), t.sidenav.style.transform = "translate3d(" + (-1 * t.width + u) + "px, 0, 0)", t.backdrop.style.opacity = d, i > a ? requestAnimationFrame(o) : (e ? (t.currentOpacity = t.sidenavOpacity, t.currentWidth = t.width) : (t.currentOpacity = 0, t.currentWidth = 0), n()) }) }) }, i.prototype.initEvents = function () { function e(e) { s.isBusy || null === o && 1 === e.touches.length && (!s.isOpened && e.touches[0].clientX > 10 || (o = e.touches[0].identifier, c = e.touches[0].clientX, a = e.touches[0].clientY, d = s.currentWidth, u = !1, l = !1, h = -999, p = -999, r.addEventListener("touchmove", t), r.addEventListener("touchcancel", n), r.addEventListener("touchend", n))) } function t(e) { for (var t = 0; t < e.changedTouches.length; t++)if (o === e.changedTouches[t].identifier) { if (Math.abs(e.changedTouches[t].clientX - h) < 1 && Math.abs(e.changedTouches[t].clientY - p) < 1) return; if (h = e.changedTouches[t].clientX, p = e.changedTouches[t].clientY, s.isOpened) { if (!l && Math.abs(c - e.changedTouches[t].clientX) < Math.abs(a - e.changedTouches[t].clientY)) return void i(null); if (l = !0, !u && e.changedTouches[t].clientX > s.currentWidth) return } return u = !0, void s.expandTo(d + (e.changedTouches[t].clientX - Math.min(c, s.width))) } } function n(e) { for (var t = 0; t < e.changedTouches.length; t++)o === e.changedTouches[t].identifier && i(u ? s.currentWidth > s.width / 1.5 : null) } function i(i) { !0 === i ? s.open() : !1 === i && s.close(), r.removeEventListener("touchmove", t), r.removeEventListener("touchcancel", n), r.removeEventListener("touchend", e), o = null } var s = this, o = null, c = null, a = null, d = null, u = !1, l = !1, h = null, p = null; s.backdrop.addEventListener("click", function () { s.close() }), r.addEventListener("touchstart", e) }
		}, {}]
	}, {}, [1])(1)
});


window.addEventListener('DOMContentLoaded', (event) => {
	"use strict";
	var sidenav = new Sidenav({
		sidenav: document.getElementById("sidenav"),
		backdrop: document.getElementById("backdropmenu")
	});
	$('#nav-icon2').click(function () {
		var hamburger = document.getElementsByClassName('open');
		if (hamburger.length == 0) {
			sidenav.open();
		} else {
			/*$('#nav-icon2').toggleClass('open');*/
				sidenav.close();
		}
	});
});


/* End Header Menu */

/* Filter mobile */
(function ($) {
	$("#filterbutton").click(function (e) {
		var Filter = $('#filters');
		// create .d-none element if it doesn't exist
		if (Filter.find(".d-none").length == 0) {
			Filter.removeClass("d-none d-md-block slideOutRight");
			Filter.addClass("slideInRight filter");
		}
	})
	$(".filterclose").click(function (e) {
		var Filter = $('#filters');
		Filter.removeClass("slideInRight");
		Filter.addClass("slideOutRight");
		setTimeout(function () {
			Filter.addClass("d-none d-md-block ");
			Filter.removeClass("filter");
		}, 500);
	});
})(jQuery);

/* Filter mobile end */

/* When the user scrolls down, hide the catalog-menu. When the user scrolls up, show the catalog-menu */
var prevScrollpos = window.pageYOffset;

var $window = $(window);
var windowsize = $window.width();
window.onscroll = function () {
	if ($('#catalog-menu').length > 0) {
		var currentScrollPos = window.pageYOffset;
		if (prevScrollpos > currentScrollPos) {
			if (windowsize < 992) {
				document.getElementById("catalog-menu").style.top = "60px";


			} else {
				if (window.pageYOffset <= 200) {
					document.getElementById("catalog-menu").style.top = "80px";
				} else {
					document.getElementById("catalog-menu").style.top = "60px";
				}
			}
		} else {
			document.getElementById("catalog-menu").style.top = "-50px";


		}
		prevScrollpos = currentScrollPos;
	}
}


/* Zoom Effects */

$(".img_producto_container")
	// tile mouse actions
	.on("mouseover", function () {
		$(this)
			.children(".img_producto")
			.css({ transform: "scale(" + $(this).attr("data-scale") + ")" });
	})
	.on("mouseout", function () {
		$(this)
			.children(".img_producto")
			.css({ transform: "scale(1)" });
	})
	.on("mousemove", function (e) {
		$(this)
			.children(".img_producto")
			.css({
				"transform-origin":
					((e.pageX - $(this).offset().left) / $(this).width()) * 100 +
					"% " +
					((e.pageY - $(this).offset().top) / $(this).height()) * 100 +
					"%"
			});
	});