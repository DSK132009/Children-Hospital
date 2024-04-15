const   express =   require('express'),
        nunjucks =  require('nunjucks'),
	    app =       express(),
        path =      require("path");
        router =    express.Router(),
        port =      Number(process.env.PORT || 8000),
        host =      '0.0.0.0';

// Nunjucks Init
nunjucks.configure('dev', {
    autoescape: true,
    express: app,
    watch:true
});


// Allow express to render static files (css/js/img/svgs/etc)
app.use(router);
// set static directories
// app.use(express.static('dev'));
app.use(express.static(path.join(__dirname, 'dev')));

// Allow all possible methods
router.all('/', (req, res, next) => {
    next();
});

const UiHelpers = {};

UiHelpers.IconHelper = (iconId, attributes) => {
    return ` <svg ${attributes ? attributes : null}><use xlink:href="/library/img/fa.svg#${iconId}"></use></svg>`;
};

// Allows only html files to be rendered by the express js server
// It will break if markup other than html is used for static sites
// And it will break if files/folder names use a '.' other than the extension.
router.get('*', (req, res, next) => {
    //Skips files that are not html
    if ( req.path.includes('.') && !req.path.endsWith('.html') ) {
        next();
    }
    //Handles folders and html files to be rendered
    else {
        let view = `App/Templates${req.path}`;

        if ( !req.path.endsWith('.html') ) {
            view += `/index.html`;
        }

        res.render(view, {UiHelpers: UiHelpers});
    }
});

// Listen
const server = app.listen(port, host, () => {
    console.log(`Listening on: http://localhost:${port}`);
});