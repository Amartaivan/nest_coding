#include <iostream>
#include <vector>

using namespace std;

#define ll long long

int main(){

    ll n;
    cin >> n;

    vector<ll> triangle(n + 1, 1);

    for (ll i = 0; i <= n; i++){
        
        ll tabs = (n * 2 + 1) - (i * 2 + 1) / 2;
        for (ll k = 0; k < tabs; k++)
                cout << '\t';
        for (ll j = 0; j <= i; j++)
            cout << triangle[j] << '\t' << '\t';
        cout << endl;

        for (ll j = i; j > 0; j--)
            triangle[j] = triangle[j] + triangle[j - 1];
    }

    return 0;
}