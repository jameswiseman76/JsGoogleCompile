function MyFunction(a) {
    if (a === 3) {
        alert(2);
    }
}

function dosomething(frm) {
    var somevar = frm.something.value;
    var err = '';
    if (somevar == '' || somevar.indexOf('@') == -1) {
        err = 'please do something';
    }

    return (err == '');
}

MyFunction(1);
MyFunction(3);
alert(dosomething());


//var x = [1, 2, 3];
//var y = x[3];