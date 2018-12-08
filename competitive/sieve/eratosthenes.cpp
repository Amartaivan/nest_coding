#include <iostream>
#include <vector>

using namespace std;

int main() {
    uint64_t n;
    cin >> n;

    vector<bool> sieve(n + 1, true);
    for (uint64_t i = 2; i <= n; ++i)
        if (sieve[i])
            for (uint64_t j = i * i; j <= n; j += i)
                sieve[j] = false;

    for (uint64_t i = 2; i <= n; ++i)
        if (sieve[i])
            cout << i << endl;
    return 0;
}