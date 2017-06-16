document.documentElement.style.fontSize = innerWidth / 16 + 'px';
onresize = function() {
	document.documentElement.style.fontSize = innerWidth / 16 + 'px';
}

var swiper = new Swiper('.swiper-container', {
	pagination: '.swiper-pagination',
	nextButton: '.swiper-button-next',
	prevButton: '.swiper-button-prev',
	paginationClickable: true,
	spaceBetween: 30,
	centeredSlides: true,
	autoplay: 2500,
	autoplayDisableOnInteraction: false
});