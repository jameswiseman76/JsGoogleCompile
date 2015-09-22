var MY_CONSTANT = 1;
MY_CONSTANT = 2;

alert(MY_CONSTANT);

var x = 1 / 0;

alert(x);

var y = 1024 * 1024 * 1024 * 2 >> 2;
alert(y);

var z = 5 >> 2.5;
alert(z);


////// Produces JSC_TRAILING_COMMA error:
////var xa = {
////    foo: 'foo',
////    bar: 'bar',
////};


function MyFunction(a) {
    if (a === 3) {
        alert(2);
    }
}

function dosomething(frm) {
    var somevar = frm.something.value;
    var somevar2 = frm.something2.value;
    var err = '';
    if (somevar == '' || somevar.indexOf('@') == -1) {
        err = 'please do something';
    }

    if (somevar2 == '' || somevar.indexOf('@') == -1) {
        err = 'please do something';
    }

    return (err == '');
}

// Produces JSC_INDEX_OUT_OF_BOUNDS_ERROR error:
var aa = [0, 1, 2];
alert(aa[3]);

MyFunction(1);
MyFunction(3);
alert(dosomething());


//var x = [1, 2, 3];
//var y = x[3];