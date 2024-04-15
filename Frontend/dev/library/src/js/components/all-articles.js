export class AllArticles {

    constructor() {
        this.allArticlesListings = document.querySelectorAll('.all-news-articles');
        if (this.allArticlesListings.length) {
            this.init();
        }
    }

    init() {
        this.allArticlesListings.forEach(element => {
            const cards = element.querySelectorAll('.news-card');
            cards.forEach((card, i) => {
                // show the first 4
                if (i >= 4) {
                    card.classList.add('d-none');
                }
            });
            element.querySelector('.load-more-btn').addEventListener('click', (e) => {
                e.preventDefault();
                
                const hiddenCards = element.querySelectorAll('.news-card.d-none');

                if (!hiddenCards.length) {
                    this.hideCards(cards, e.target);
                } else {
                    this.showMoreCards(hiddenCards, e.target);
                }
            });
        });
    }

    showMoreCards(cards, element) {
        if (cards.length <= 4) {
            element.innerHTML = 'Show Less';
        }

        Array.from(cards).slice(0, 4).map(card => {
            card.classList.remove('d-none');
        });
    }

    hideCards(cards, element) {
        element.innerHTML = 'Load More';

        Array.from(cards).slice(4).map(card => {
            card.classList.add('d-none');
        });
    }
}