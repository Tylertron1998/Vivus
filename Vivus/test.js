for(let i = 0; i < 10000; i++) {
    console.log("Writing: " + i);
}

require('fs').writeFile('test.txt', 'kekkers');