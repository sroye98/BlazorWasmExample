var gulp = require('gulp');
var rename = require('gulp-rename');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');

gulp.task('sass', function () {
    return gulp.src('wwwroot/scss/style.scss')
        .pipe(sass({
            outputStyle: 'compressed'
        }).on('error', sass.logError))

        .pipe(sourcemaps.write())

        .pipe(sourcemaps.init({
            loadMaps: true
        }))

        .pipe(rename({ extname: '.min.css' }))

        .pipe(gulp.dest('wwwroot/css'))
});