/*
Fermat's Little Theorem:
If n is a prime number, then for every a, 1 <= a < n,

a**n-1 ≡ 1 (mod n)
 OR 
a**n-1 % n = 1 
*/

#include <iostream>

#include <cstdlib>
#include <ctime>

using namespace std;

typedef unsigned long long ull_t;

ull_t pow(ull_t x, ull_t exp, ull_t mod) {
    ull_t b = x, result = 1;

    while (exp > 0) {
        if (exp & 1)
            result = result * b % mod;
        b = b * b % mod;
        exp >>= 1;
    }

    return result;
}
ull_t log2(ull_t x) {
    ull_t result = 0;

    while (x) {
        x >>= 1;
        result++;
    }

    return result;
}

bool fermat(ull_t n, int i) {
    while (i--) {
        int a = rand() % n;
        while (a < 2 || a == n - 1)
            a = rand() % n;

        if (pow(a, n - 1, n) != 1)
            return false;
    }
    return true;
}

int main() {
    srand(time(NULL));

    ull_t a;
    cin >> a;

    cout << (fermat(a, log2(a)) ? "Probably prime" : "Not prime") << endl;
    return 0;
}