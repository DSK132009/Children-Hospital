export class SiteHeader {

    static main() {

        // On Desktop, show nav dropdown when mouseover event occurs
        $('.site-header .nav-item.dropdown').on('mouseover', function(event) {

            if (window.innerWidth < 1200)
                return;

            const listItem = event.currentTarget;

            if (!listItem.classList.contains('dropdown'))
                return;

            const position = listItem.getBoundingClientRect();
            let dropdown = listItem.querySelector('.dropdown-menu');

            if (position.left + dropdown.offsetWidth > window.innerWidth) {
                dropdown.classList.add('right-align');
            } else {
                dropdown.classList.remove('right-align');
            }

            listItem.querySelector('.nav-link.dropdown-toggle').classList.add('show');
            dropdown.classList.add('show');
        });

        // On Desktop, hide nav dropdown when mouse leave event occurs
        $('.site-header .nav-item.dropdown').on('mouseleave', function(event) {
            if (window.innerWidth < 1200)
                return;

            let listItem = event.currentTarget;

            if (!listItem.classList.contains('dropdown'))
                return;

            let dropdown = listItem.querySelector('.dropdown-menu');

            dropdown.classList.remove('show');
            listItem.querySelector('.nav-link.dropdown-toggle').classList.remove('show');
        });

        // On first link keydown, tab display dropdown, shift tab hide dropdown
        $('.site-header .nav-item.dropdown .nav-link').on('keydown', function(event) {
            if (window.innerWidth < 1200)
                return;

            let listItem = event.target.closest('.nav-item.dropdown');
            let position = listItem.getBoundingClientRect();
            let dropdown = listItem.querySelector('.dropdown-menu');

            if ((event.key === 'Tab' || event.code === 'Tab') && event.shiftKey) {

                dropdown.classList.remove('show');
                listItem.querySelector('.nav-link.dropdown-toggle').classList.remove('show');

            } else if (event.key === 'Tab' || event.code === 'Tab') {

                if (position.left + dropdown.offsetWidth > window.innerWidth) {
                    dropdown.classList.add('right-align');
                } else {
                    dropdown.classList.remove('right-align');
                }

                dropdown.classList.add('show');
                listItem.querySelector('.nav-link.dropdown-toggle').classList.add('show');
            }
        });

        // On last link focus, hide dropdown when tabbed away
        $('.site-header .nav-item.dropdown .dropdown-menu li:last-child .dropdown-item').on('keydown', function(event) {

            if (window.innerWidth < 1200)
                return;

            if ((event.key === 'Tab' || event.code === 'Tab')  && !event.shiftKey) {
                let listItem = event.target.closest('.nav-item.dropdown');
                let dropdown = listItem.querySelector('.dropdown-menu');

                dropdown.classList.remove('show');
                listItem.querySelector('.nav-link.dropdown-toggle').classList.remove('show');
            }
        });

        // On mobile menu btn click, open menu
        $('.site-header-menu-btn').on('click', function() {

            $('.site-header-search').removeClass('show');

            const searchIcon = $('.site-header-search-toggle').attr('data-icon');
            $('.site-header-search-toggle use').attr('xlink:href', searchIcon);

            $('.site-header-nav').toggleClass('show');

            if ($('.site-header-nav').hasClass('show')) {
                const closeIcon = $(this).attr('data-close-icon');
                $(this).find('use').attr('xlink:href', closeIcon);
            } else {
                const icon = $(this).attr('data-icon');
                $(this).find('use').attr('xlink:href', icon);
            }
        });

        // On mobile search btn click, open search display
        $('.site-header-search-toggle').on('click', function() {

            $('.site-header-search input').val('');
            $('.site-header-nav').removeClass('show');

            const menuIcon = $('.site-header-menu-btn').attr('data-icon');
            $('.site-header-menu-btn use').attr('xlink:href', menuIcon);

            $('.site-header-search').toggleClass('show');

            if ($('.site-header-search').hasClass('show')) {
                const closeIcon = $(this).attr('data-close-icon');
                $(this).find('use').attr('xlink:href', closeIcon);
                $('.site-header-search input').focus();
            } else {
                const icon = $(this).attr('data-icon');
                $(this).find('use').attr('xlink:href', icon);
            }
        });
    }
}