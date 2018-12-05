/**
 * Module dependencies.
 */

const cluster = require('cluster'),
    numCPUs = require('os').cpus().length,
    express = require('express'),
    pgp = require('pg-promise')(),
    bodyParser = require('body-parser');

const connection = {
    db: process.env.db || 'benchmark',
    username: process.env.username || 'benchmark',
    password: process.env.password || 'benchmark',
    host: process.env.host || 'localhost',
    port: process.env.port || '5432',
    dialect: 'postgres',
}

const connectionString = `postgres://${connection.username}:${connection.password}@${connection.host}:${connection.port}/${connection.db}`
const db = pgp(connectionString);


const getAllFortunes = async () => {
    return await db.many('select id,message from fortune', [true]);
};

if (cluster.isMaster) {
    console.log(`CPUS: ${numCPUs}`);
    console.log(`Database: ${connectionString}`);
    // Fork workers.
    for (let i = 0; i < numCPUs; i++) {
        cluster.fork();
    }

    cluster.on('exit', (worker, code, signal) =>
        console.log('worker ' + worker.pid + ' died'));
} else {
    const app = module.exports = express();

    // Configuration
    app.set('view engine', 'pug');
    app.set('views', __dirname + '/views');
    app.use(bodyParser.urlencoded({ extended: true }));
    app.use(bodyParser.json());

    // Set headers for all routes
    app.use((req, res, next) => {
        res.setHeader("Server", "Express");
        return next();
    });

    app.get('/plaintext', (req, res) =>
        res.header('Content-Type', 'text/plain').send('Hello, World!'));

    app.get('/fortunes', async (req, res) => {
        let fortunes = await getAllFortunes()
        const newFortune = { id: 0, message: "Additional fortune added at request time." };
        fortunes.push(newFortune);
        fortunes.sort((a, b) => (a.message < b.message) ? -1 : 1);
        res.render('fortunes/index', { fortunes: fortunes });
    });

    app.listen(80);
}