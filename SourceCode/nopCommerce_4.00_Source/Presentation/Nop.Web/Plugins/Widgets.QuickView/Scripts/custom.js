$(document).ready(function () {

    //$("#myBtn").click(function () {
      
    //    $("#myModal").modal();

    //});
    var product;
    var access_token = 'eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1MjU5NDg3MDksImV4cCI6MTg0MTMwODcwOSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDoxNTUzNiIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjE1NTM2L3Jlc291cmNlcyIsIm5vcF9hcGkiXSwiY2xpZW50X2lkIjoiOTc1YzBhNDctYjViYS00Mzk1LWFhMTYtMTM3OTk2Nzg4NmJlIiwic3ViIjoiOTc1YzBhNDctYjViYS00Mzk1LWFhMTYtMTM3OTk2Nzg4NmJlIiwiYXV0aF90aW1lIjoxNTI1OTQ4NzA5LCJpZHAiOiJsb2NhbCIsInNjb3BlIjpbIm5vcF9hcGkiLCJvZmZsaW5lX2FjY2VzcyJdLCJhbXIiOlsicHdkIl19.Z6LIlRO0xUNUy_r9igkLuSNU4RdSEamF8LofQQNjePmdh9xOMSJgOSCenM648otF7CHo_kAOmY3SkSc01QKYQ_pGzOSuIFI0kdonp2FO_OggBM0O1sSSE6vTeepdaFDnvNFwr7V5FLLVai9Mxuam-1XJumtEBs2OyQlYjdn-j092Dqb-inANJ5bI6vZjxCluPw0ARozFk7ymBDVgU-yvPz_sKY-DtWWiQBgTHUGbCnzt48hVg4WN3072UjuORSPlhtrkZNroWOzDVfc6oTsnQT8nWcS9-K5rmi6S8MVnmNg0eUk4Iv2pK27jIrlwzZD5SjOELX08VnPAr4SO3U9eNA';
    var pathname = window.location.origin;
    var settings2 = {
        "async": true,
        "crossDomain": true,
        "url": pathname + "/api/products",
        "method": "GET",
        "headers": {
            "authorization": "Bearer " + access_token,
            "cache-control": "no-cache",
            //"postman-token": access_token
        },
    }
    $.ajax(settings2).done(function (response) {
        console.log("--------api-1----------");
        console.log(response);
    });

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": pathname + "/api/productdetails/1",
        "method": "GET",
        "headers": {
            "NST": "eyJhbGciOiJIUzUxMiJ9.eyJOU1RfS0VZIjoiYm05d1UzUmhkR2x2YmxSdmEyVnUifQ.adqiIzFjqZdpJw5uHOHjE5qw2UvCDH2FwMmwlYvr5ljKyPG65ZQe_4wb8NYEQFXmyZZyVu-77xd5Njn310cjMw",
            "Token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJDdXN0b21lcklkIjoyMjA2LCJleHAiOjE1NDAzNzk2ODN9.giJjwt7JycJMYYEk_f4RTDsgyQSlRjp-oNrW9yL1yWE",
            "DeviceId": "001faebc1f794fac"
        }
    }
    $.ajax(settings).done(function (response) {
        console.log("--------api-2 xxxx----------");
        product = response.Data;
		console.log(product);
    });
   $("#test").on('click', function () {
        $.fancybox.open({
            src: product.Name,
            type: 'html',
            smallBtn: false
        });

    });

});