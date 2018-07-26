#include <iostream>

using namespace std;

int main(){
	
	int n, result = 0, i, j;
	cin >> n;
	for (i = 0; i < n; i++)
		cin >> j; //Just read, we needn't these values
	
	result = (n + 1) * n / 2 - n;
			
	cout << result << endl;
	return 0;
}
