var gulp = require('gulp'); gulp.task('copy-typings', function () {
    return gulp.src('wwwroot/jspm_packages/**/*.d.ts')
    .pipe(gulp.dest('scripts/typings/jspm_packages/'));
});