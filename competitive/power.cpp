#include <iostream>
using namespace std;

int main(){
	unsigned long long a, b, result = 1;
	cin >> a >> b;
	
	while (b > 0){
		if (b % 2 == 1)
			result *= a;
		a *= a;
		b /= 2;
	}
	
	cout << result << endl;
	return 0;
}
