import { _KEYBOARD_ } from '../utils/utils';

export class QuickLinksDropdowns {

    static main() {

        function openDropdown(element) {

            $('.quick-link-dropdown.show').removeClass('show');
            $('.quick-dropdown-menu.show').removeClass('show');

            const dropdown = $(element).closest('.quick-link-dropdown').find('.quick-dropdown-menu');
            $(element).closest('.quick-link-dropdown').addClass('show');
            // $(element).addClass('show');
            $(dropdown).addClass('show');
        }

        function closeDropdown(element) {

            const dropdown = $(element).closest('.quick-link-dropdown').find('.quick-dropdown-menu');
			
			if(!$(element).closest('.quick-link-dropdown').hasClass('current')) {				
				
				$(element).closest('.quick-link-dropdown').removeClass('show');
				// $(element).removeClass('show');
				$(dropdown).removeClass('show');
			
			}
        }

        $('.quick-link-dropdown').each(function(index, element) {
            $(element).on('click', function(event) {
                
                if (window.innerWidth > 1200)
                    return;
                const linkItem = event.currentTarget;
                if ($(linkItem).closest('.quick-link-dropdown').hasClass('show')) { 
                    closeDropdown(linkItem);
                } else {
                    openDropdown(linkItem);
                }
            });
            
            $(element).on('mouseenter', function(event) {
                if (window.innerWidth < 1200)
                    return;

                const linkItem = event.currentTarget;

                if ($(linkItem).closest('.quick-link-dropdown').hasClass('show')) {
                    return;
                } else {   
                    openDropdown(linkItem);
                }
            });

            $(element).on('mouseleave', function(event) {
                
                if (window.innerWidth < 1200)
                    return;

                const linkItem = event.currentTarget;

                if (!$(linkItem).closest('.quick-link-dropdown').hasClass('show')) {
                    return;
                } else {   
                    closeDropdown(linkItem);
                }
            });

            $(element).on('keyup', function(event) {
                const linkItem = event.currentTarget;

                if (event.which === _KEYBOARD_.enter) {
                    openDropdown(linkItem);   
                } else if (event.which === _KEYBOARD_['arrow-u']) {
                    closeDropdown(linkItem);
                } else if (event.which === _KEYBOARD_['arrow-d']) {
                    openDropdown(linkItem);
                } else {
                    return false;
                }
            });
        });
    }
}