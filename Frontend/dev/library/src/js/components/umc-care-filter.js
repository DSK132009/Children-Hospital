export class UmcCareFilter {

    static main () {

        const locations = document.querySelectorAll('.location-card');
        const checkboxes = Array.from(document.querySelectorAll('.form-check-input'));
        
        checkboxes.forEach((input) => {
            input.addEventListener('click', (event) => {
                
                if (document.querySelector('.form-check-input.active')) {  
                    document.querySelector('.form-check-input.active').classList.remove('active');
                }

                input.classList.add('active');
                const catagory = input.getAttribute('value');
                
                switch(catagory) {
                    case 'all':
                        document.querySelector('.PRIMARY-list').classList.remove('d-none');
                        document.querySelector('.QUICK-list').classList.remove('d-none');
                        break;
                    case 'primary-care':
                        document.querySelector('.PRIMARY-list').classList.add('d-block');
                        document.querySelector('.PRIMARY-list').classList.remove('d-none');
                        document.querySelector('.QUICK-list').classList.add('d-none');
                        document.querySelector('.QUICK-list').classList.remove('d-block');
                        break;
                    case 'quick-care':         
                        document.querySelector('.QUICK-list').classList.add('d-block');
                        document.querySelector('.QUICK-list').classList.remove('d-none');
                        document.querySelector('.PRIMARY-list').classList.add('d-none');
                        document.querySelector('.PRIMARY-list').classList.remove('d-block');
                        break;
                    default:
                        document.querySelectorAll('.clinic-location-list.d-none').classList.remove('d-none');
                }
            });
        });
    }
}