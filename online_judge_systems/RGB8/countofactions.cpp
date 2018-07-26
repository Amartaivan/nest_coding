#include <iostream>

using namespace std;

int main(){
	
	int n, result = 0, i, j;
	cin >> n;
	for (i = 0; i < n; i++)
		cin >> j; //Just read, we needn't these values
	
	for (i = 0; i < n; i++)
		for (j = i + 1; j < n; j++)
			result++;
			
	cout << result << endl;
	return 0;
}
