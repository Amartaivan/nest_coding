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
bool miller_rabin(ull_t n, int k) {
    if (n == 2 || n == 3)
        return true;
    if (n <= 1 || !(n & 1))
        return false;

    ull_t s = 0;
    for (uint64_t x = n - 1; !(x & 1); ++s, x >>= 1);

    ull_t t = (n - 1) / (1 << s);

    while (k--) {
        ull_t a = rand() % (n - 4) + 2;
        ull_t x = pow(a, t, n);

        if (x == n - 1 || x == 1)
            continue;

        for (ull_t i = 0; i < s - 1; ++i) {
            x = pow(x, 2, n);

            if (x == 1)
                return false;
            if (x == n - 1)
                goto loop;
        }
    loop:
        continue;
    }

    return true;
}


int main() {
    srand(time(NULL));

    ull_t a;
    cin >> a;

    cout << (miller_rabin(a, log2(a)) ? "Probably prime" : "Not prime") << endl;
    return 0;
}