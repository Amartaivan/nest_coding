#include <functional>
#include <iostream>
#include <fstream>
#include <random>
#include <chrono>

using namespace std;

int main(){

    default_random_engine generator(chrono::system_clock::now().time_since_epoch().count());
    uniform_int_distribution<int> distribution(1,329787343);
    auto _rand = bind(distribution, generator);

    int n = _rand();
    cout << n << endl;
    for (int i = 0; i < n; i++)
        cout << _rand() << ' ';
    return 0;
}