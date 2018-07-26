#include <iostream>

using namespace std;

int main(){
    int n, m;
    cin >> n >> m;
	
	m = abs(m);
	n = abs(n);

    while (n > 0 && m > 0){
        if (n > m)
            n = n % m;
        else
            m = m % n;
    }

    cout << n + m << endl;
    return 0;
}