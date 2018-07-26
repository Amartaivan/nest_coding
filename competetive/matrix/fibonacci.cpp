#include "matrix.h"

matrix_t multiply(matrix_t a, matrix_t b){
    ull n = a.size();
    matrix_t result(n, vector<ull>(n));

    for (ull i = 0; i < n; i++){
        for (ull j = 0; j < n; j++){
            ull sum = 0;

            for (ull k = 0; k < n; k++)
                sum += a[i][k] * b[k][j];

            result[i][j] = sum;
        }
    }

    return result;
}

matrix_t power(matrix_t a, ull k){
    ull n = a.size();
    matrix_t result = identity;

    while (k > 0){
        if (k & 1 == 1)
            result = multiply(result, a);

        a = multiply(a, a);
        k = k >> 1;
    }

    return result;
}

int main(){
    ull n;
    cin >> n;

    n--;
    if (n < 1){
        cout << 1 << endl;
        return 0;
    }

    matrix_t m1 = {{2, 1}, {1, 1}}, m2 = {{1, 1}, {1, 0}};
    m2 = power(m2, n - 1);

    matrix_t result = multiply(m1, m2);
#ifdef DEBUG
    for (auto a : result){
        for (auto b : a)
            cout << b << ' ';
        cout << endl;
    }
#endif
    cout << result[0][0] << endl;
    return 0;
}