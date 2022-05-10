const path = require('path');

module.exports = {
	entry: {
		data_management: {
			import: './React/data-management.ts',
			dependOn: [ 'shared' ]
		},
		schedule_arranger: {
			import: './React/schedule-arranger.ts',
			dependOn: [ 'shared' ]
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
			}
		],
	},
	resolve: {
		extensions: ['.tsx', '.ts', '.js'],
	},
	devtool: "source-map"
};