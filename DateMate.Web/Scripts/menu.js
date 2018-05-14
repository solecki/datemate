$(() => {
	// Hover animation for menu items.
	let menuMouseEnter = function () {
		$(this).stop().animate({
			backgroundColor: '#0f7f8e',
			paddingTop: '1.5em'
		}, 300);
	};
	let menuMouseOut = function () {
		$(this).stop().animate({
			backgroundColor: '#3C6E71',
			paddingTop: '1em'
		}, 300);
	};

	let menuItems = document.getElementsByClassName('menu-item');
	for (var i = 0; i < menuItems.length; i++) {
		menuItems[i].addEventListener('mouseenter', menuMouseEnter);
		menuItems[i].addEventListener('mouseout', menuMouseOut);
	}

	// Mobile menu open and close animation.
	let $hamburgerIcon = $('#hamburger-icon');
	let $menu = $('#menu');
	document.getElementById('hamburger-icon').addEventListener('click', function () {
		if ($hamburgerIcon.hasClass('hamburger-open')) {
			// Menu closing.
			$hamburgerIcon.toggleClass('hamburger-open');
			$menu.animate({
				top: '0px',
				opacity: '0'
			}, {
					duration: 400,
					complete: function () {
						$menu.removeAttr('style');
						// Toggle classes after animation is done.
						$menu.toggleClass('open');
						$('.menu-item').toggleClass('menu-item-mobile');
					}
				});
		} else {
			// Menu opening.
			$menu.css('opacity', 0);
			$hamburgerIcon.toggleClass('hamburger-open');
			$menu.animate({ top: '42px', opacity: '1' }, 400);
			$('.menu-item').toggleClass('menu-item-mobile');
			$menu.toggleClass('open');
		}
	});
});