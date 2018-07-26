#include <iostream>

using namespace std;

int main(){
    long n, m, gcd, sf = 0;
    cin >> n >> m;
	
	m = abs(m);
	n = abs(n);

    if (n > m)
        gcd = m;
    else
        gcd = n;
    
    for (; gcd > 0 && sf == 0; gcd--)
        if (n % gcd == 0 && m % gcd == 0)
            sf = 1;

    gcd++;
    cout << gcd << endl;//cout << ++gcd << endl;
    return 0;
}