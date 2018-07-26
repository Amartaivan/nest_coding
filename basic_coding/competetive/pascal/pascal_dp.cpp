#include <iostream>

using namespace std;

#define ll long long

int main(){

    ll n;
    cin >> n;
    
    for (ll i = 0; i <= n; i++){
        ll current = 1;

        ll tabs = (n * 2 + 1) - (i * 2 + 1) / 2;
        for (ll k = 0; k < tabs; k++)
                cout << '\t';
        cout << current << '\t' << '\t';
        for (ll j = 0; j < i; j++){
            current = current * (i - j) / (j + 1);
            cout << current << '\t' << '\t';
        }
        cout << endl;
    }
    return 0;
}