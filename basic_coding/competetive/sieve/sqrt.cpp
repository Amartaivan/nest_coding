#include <iostream>
#include <vector>

#include <cstdint>
#include <cmath>

using namespace std;

vector<uint64_t> primes = {2, 3, 5, 7};

bool isPrime(const uint64_t& number) {
    uint64_t limit = sqrt(number);

    for (uint64_t prime : primes)
        if (prime > limit)
            return true;
        else if (number % prime == 0)
            return false;
}

int main() {
    uint64_t n;
    cin >> n;
    
    uint64_t i = 10;
    while (i < n) {
        if (isPrime(i))
            primes.push_back(i);
        ++i;
    }

    for (uint64_t prime : primes)
        cout << prime << endl;
    return 0;
}