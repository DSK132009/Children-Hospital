const sass = require('node-sass');
const externalGlobals = require('rollup-plugin-external-globals');
const resolve = require('@rollup/plugin-node-resolve');

module.exports = function (grunt) {
    
    require('load-grunt-tasks')(grunt, {scope: 'devDependencies'});
    
    // Inform that a file was changed
    grunt.event.on('watch', function(action, filepath, target) {
      grunt.log.writeln(target + ': ' + filepath + ' has ' + action);
    });

    // Grunt Init
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'), //read in the package data

        // ------------------------------------------------
        // nunjucks
        // Description: compiles nunjuck templates into html and copies /library
        // ------------------------------------------------
        nunjucks: {
            options: {
                data: 'Frontend Boilerplate With Nunjucks',
                paths: ['dev/App', 'dev/App/MasterPages']
            },
            render: {
                files: [{
                    expand: true,
                    cwd: 'dev/App/Templates',
                    src: [
                        '**/*.html',
                        '!**/_*.html'
                    ],
                    dest: './static_html',
                    ext: '.html'
                }]
            }
        },

        // ------------------------------------------------
        // Express
        // Description: starts up a connect server with live reload
        // ------------------------------------------------
        express: {
            dev: {
                options: {
                    hostname: '*',
                    port: grunt.option('port') || 8000,
                    livereload: true,
                    script: 'index.js'
                }
            }
        },

        // ------------------------------------------------
        // Watch
        // Description: watches scss and js source directories for changes
        // ------------------------------------------------
        watch: {
            options: {
                spawn: false,
                reload: true
            },
            css: {
                files: '<%= pkg.path.src.scss %>/**/*.scss',
                tasks: ['css']
            },
            js: {
                files: ['<%= pkg.path.src.js %>/**/*.js'],
                tasks: ['js']
            }
        },

        // ------------------------------------------------
        // Sass
        // Description: compiles .scss files into .css file(s)
        // ------------------------------------------------
        sass: {
            options: {
                implementation: sass,
                sourceMap: true,
                outputStyle: 'expanded'
            },
            dist: {
                files: {
                    '<%= pkg.path.dest.css %>/styles.css': '<%= pkg.path.src.scss %>/styles.scss'
                }
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // Autoprefixer
        // Description: add vendor prefixes to the compiled .css file(s)
        // ------------------------------------------------
        autoprefixer: {
            options: {
                browsers: ['last 7 versions'],
                map: true
            },
            prod: {
                src: '<%= pkg.path.dest.css %>/styles.css',
                dest: '<%= pkg.path.dest.css %>/styles.css'
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // CSS Min
        // Description: minifies .css files
        // ------------------------------------------------
        // cssmin: {
        //     cutup: {
        //         keepSpecialComments: true,
        //         expand: true,
        //         cwd: '<%= pkg.path.dest.css %>',
        //         src: ['*.css', '!*.min.css'],
        //         dest: '<%= pkg.path.dest.css %>',
        //         ext: '.min.css'
        //     }
        // },
        // ------------------------------------------------


        // ------------------------------------------------
        // JS Hint
        // Description: checks JS code for errors
        // ------------------------------------------------
        jshint: {
            all: [
                '<%= pkg.path.src.js %>/**/*.js',
                '!<%= pkg.path.src.js %>/bootstrap/*.js'
            ],
            options: {
                esversion: 6
            }
        },
        // --------------------------------------------

        // ------------------------------------------------
        // Rollup
        // Description: Ecma Script 6 Javascript module bundler
        // -
        rollup: {
            options: {
                plugins: [
                    externalGlobals({
                        jquery: "$"
                    }),
                    resolve({
                        only: ['popper.js']
                    })
                ]
            },
            main: {
                'dest': 'dev/library/js/main.js',
                'src': 'dev/library/src/js/main.js'
            },
            bootstrap: {
                'dest': 'dev/library/js/bootstrap-build.js',
                'src': 'dev/library/src/js/bootstrap-build.js'
            }
        },
        // ------------------------------------------------

        // ------------------------------------------------
        // Babel
        // Description: compiles js files from ES6 using Babel
        // ------------------------------------------------
        babel: {
            options: {
                sourceMap: true,
                presets: ['babel-preset-env'],
                plugins: ["transform-object-rest-spread"]
            },
            dist: {
                files: {
                    'dev/library/js/main.js': 'dev/library/js/main.js',
                    'dev/library/js/bootstrap-build.js': 'dev/library/js/bootstrap-build.js'
                }
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // Uglify
        // Description: minifies JS file
        // ------------------------------------------------
        // uglify: {
        //     options: {
        //         mangle: {
        //             reserved: ['jQuery']
        //         },
        //         preserveComments: 'none'
        //     },
        //     my_target: {
        //         files: {
        //             '<%= pkg.path.dest.js %>/main.js': ['<%= pkg.path.dest.js %>/main.js']
        //         }
        //     }
        // },
        // ------------------------------------------------


        // ------------------------------------------------
        // Copy
        // Description: library to dest
        // ------------------------------------------------
        copy: {
            devToPublic: {
                files: [{ 
                    expand: true, 
                    cwd: "dev/library/", 
                    src:    [ "**", "!src/**"], 
                    dest: "../MVC/wwwroot/public/library/"
                }]                
            },
            devToStaticHTML: {
                files: [{ 
                    expand: true, 
                    cwd: "dev/library/", 
                    src: [ "**", "!src/**"], 
                    dest: "static_html/library/" 
                }]                
            }
        },
        // ------------------------------------------------

        

        browserSync: {
            dev: {
                bsFiles: {
                    src : [
                        '<%= pkg.path.dest.css %>/*.css', 
                        '<%= pkg.path.dest.js %>/*.js',
                        '<%= pkg.path.src.html %>/**/*.html'
                    ]
                },
                options: {
                    proxy: `http://localhost:${grunt.option('port') || 8000}/`,
                    watchTask: true
                }
            }
        },

        

    });

    grunt.loadNpmTasks('grunt-browser-sync');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-autoprefixer');
    grunt.loadNpmTasks('grunt-nunjucks-2-html');

    grunt.registerTask('css', ['sass', 'autoprefixer', 'copy']);
    grunt.registerTask('js', ['jshint', 'rollup', 'babel', 'copy']);
    grunt.registerTask('html', ['nunjucks', 'copy']);
    grunt.registerTask('default', ['css', 'js', 'html', 'browserSync', 'express', 'watch']);
    grunt.registerTask('STBuild', ['css', 'js', 'html']);
};