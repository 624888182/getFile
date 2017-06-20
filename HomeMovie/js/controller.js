app
	//	首页
	.controller('HomeCtrl', function() {
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
	})
	
	//  大陆电影
	.controller('MainlandCtrl', function() {
		var galleryTop = new Swiper('.gallery-top', {
			nextButton: '.swiper-button-next',
			prevButton: '.swiper-button-prev',
			spaceBetween: 10,
		});
		var galleryThumbs = new Swiper('.gallery-thumbs', {
			spaceBetween: 10,
			centeredSlides: true,
			slidesPerView: 'auto',
			touchRatio: 0.2,
			slideToClickedSlide: true
		});
		galleryTop.params.control = galleryThumbs;
		galleryThumbs.params.control = galleryTop;
		
		
		$("#Mainland .Main_new").click(function(){
			$("#Mainland_menu_one").css("display","block");
			$("#Mainland_menu_two").css("display","none");
		});
		$("#Mainland .Main_btn").click(function(){
			$("#Mainland_menu_one").css("display","none");
			$("#Mainland_menu_two").css("display","block");
		});
	})
	
	//  港台电影
	.controller('HongkongCtrl', function() {
		var galleryTop = new Swiper('.gallery-top', {
			nextButton: '.swiper-button-next',
			prevButton: '.swiper-button-prev',
			spaceBetween: 10,
		});
		var galleryThumbs = new Swiper('.gallery-thumbs', {
			spaceBetween: 10,
			centeredSlides: true,
			slidesPerView: 'auto',
			touchRatio: 0.2,
			slideToClickedSlide: true
		});
		galleryTop.params.control = galleryThumbs;
		galleryThumbs.params.control = galleryTop;
		
		
		$("#Hongkong .Main_new").click(function(){
			$("#Hongkong_menu_one").css("display","block");
			$("#Hongkong_menu_two").css("display","none");
		});
		$("#Hongkong .Main_btn").click(function(){
			$("#Hongkong_menu_one").css("display","none");
			$("#Hongkong_menu_two").css("display","block");
		});
	})
	
//  欧美电影
	.controller('AmericaCtrl', function() {
		var galleryTop = new Swiper('.gallery-top', {
			nextButton: '.swiper-button-next',
			prevButton: '.swiper-button-prev',
			spaceBetween: 10,
		});
		var galleryThumbs = new Swiper('.gallery-thumbs', {
			spaceBetween: 10,
			centeredSlides: true,
			slidesPerView: 'auto',
			touchRatio: 0.2,
			slideToClickedSlide: true
		});
		galleryTop.params.control = galleryThumbs;
		galleryThumbs.params.control = galleryTop;
		
		
		$("#America .Main_new").click(function(){
			$("#America_menu_one").css("display","block");
			$("#America_menu_two").css("display","none");
		});
		$("#America .Main_btn").click(function(){
			$("#America_menu_one").css("display","none");
			$("#America_menu_two").css("display","block");
		});
	})
	
//  动漫电影
	.controller('CartoonCtrl', function() {
		var galleryTop = new Swiper('.gallery-top', {
			nextButton: '.swiper-button-next',
			prevButton: '.swiper-button-prev',
			spaceBetween: 10,
		});
		var galleryThumbs = new Swiper('.gallery-thumbs', {
			spaceBetween: 10,
			centeredSlides: true,
			slidesPerView: 'auto',
			touchRatio: 0.2,
			slideToClickedSlide: true
		});
		galleryTop.params.control = galleryThumbs;
		galleryThumbs.params.control = galleryTop;
		
		
		$("#Cartoon .Main_new").click(function(){
			$("#Cartoon_menu_one").css("display","block");
			$("#Cartoon_menu_two").css("display","none");
		});
		$("#Cartoon .Main_btn").click(function(){
			$("#Cartoon_menu_one").css("display","none");
			$("#Cartoon_menu_two").css("display","block");
		});
	})