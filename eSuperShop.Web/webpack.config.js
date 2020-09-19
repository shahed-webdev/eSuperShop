const path = require('path');
const webpack = require('webpack');
const fs = require('fs');
const { VueLoaderPlugin } = require('vue-loader');
const TerserPlugin = require('terser-webpack-plugin');

const appBasePath = './VueJS/'; // where the source files located
const publicPath = '../bundle/'; // public path to modify asset urls. eg: '../bundle' => 'www.example.com/bundle/main.js'
const bundleExportPath = './wwwroot/bundle/'; // directory to export build files
const jsEntries = {}; // listing to compile

// We search for js files inside basePath folder and make those as entries
fs.readdirSync(appBasePath).forEach(function(name) {
    // assumption: modules are located in separate directory and each module component is imported to index.js of particular module
    const indexFile = appBasePath + name + '/index.js';
    if (fs.existsSync(indexFile)) {
        jsEntries[name] = indexFile
    }
});

module.exports = {
    mode: 'none',
    entry: jsEntries,
    output: {
        path: path.resolve(__dirname, bundleExportPath),
        publicPath: publicPath,
        filename: '[name].js'
    },
    resolve: {
        extensions: ['.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js',
            '@': path.join(__dirname, appBasePath)
        }
    },
    module: {
        rules: [{
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    rules: {
                        scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                        sass: 'vue-style-loader!css-loader!sass-loader?indentedSyntax' // <style lang="sass">
                    }
                }
            },
            {
                test: /\.scss$/,
                loader: 'style-loader!css-loader!sass-loader'
            },
            {
                test: /\.css$/,
                loader: 'style-loader!css-loader'
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2)(\?\S*)?$/,
                loader: 'file-loader'
            },
            {
                test: /\.(png|jpe?g|gif|svg)(\?\S*)?$/,
                loader: 'file-loader',
                query: {
                    name: '[name].[ext]?[hash]'
                }
            }
        ]
    },
    plugins: [new VueLoaderPlugin()],
    optimization: {
        minimize: true,
        minimizer: [new TerserPlugin()]
    },
    devtool: '#source-map' //'#eval-source-map'
}

module.exports.watch = process.env.WATCH === "true";

if (process.env.NODE_ENV === 'production') {
  module.exports.devtool = '#source-map'
  module.exports.plugins = (module.exports.plugins || []).concat([
    new webpack.DefinePlugin({
      'process.env': { NODE_ENV: '"production"' }
    })
  ]);
}
else if (process.env.NODE_ENV === "dev") {
    module.exports.watch = true;
    module.exports.plugins = (module.exports.plugins || []).concat([
        new webpack.DefinePlugin({
            'process.env': { NODE_ENV: '"development"' }
        })
    ]);
}

