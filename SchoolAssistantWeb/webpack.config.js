const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
	entry: {
		data_management: {
			import: './React/data-management.ts',
			dependOn: [ 'react_lib', 'shared' ]
		},
		schedule_arranger: {
			import: './React/schedule-arranger.ts',
			dependOn: [ 'react_lib', 'shared', 'schedule_shared' ]
		},
		schedule_display: {
			import: './React/schedule-display.ts',
			dependOn: [ 'react_lib', 'shared', 'schedule_shared' ]
		},
		schedule_shared: {
			import: './React/schedule-shared.ts',
			dependOn: [ 'react_lib', 'shared' ]
		},
		shared: {
			import: './React/shared.ts'
		},
		react_lib: './React/react-lib.ts'
	},
	output: {
		filename: '[name].bundle.js',
		globalObject: 'this',
		path: path.resolve(__dirname, 'wwwroot/dist'),
		publicPath: '/dist/'
	},
	mode: process.env.NODE_ENV === 'production' ? 'production' : 'development',
	optimization: {
		runtimeChunk: {
			name: 'runtime', // necessary when using multiple entrypoints on the same page
		},
		splitChunks: {
			cacheGroups: {
				commons: {
					test: /[\\/]node_modules[\\/](react|react-dom)[\\/]/,
					name: 'vendor',
					chunks: 'all',
				},
			},
		},
	},
	module: {
		rules: [
			{
				test: /\.tsx?$/,
				use: 'ts-loader',
				exclude: /node_modules/,
			},
			{
				test: /\.css$/,
				use: [
					{ loader: MiniCssExtractPlugin.loader },
					{ loader: 'css-loader', options: { importLoaders: 1 } }
				]
			}
		],
	},
	resolve: {
		extensions: ['.tsx', '.ts', '.js'],
	},
	plugins: [
		new MiniCssExtractPlugin()
	],
	devtool: "source-map"
};