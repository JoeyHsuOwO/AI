#include <iostream>
using namespace std;

struct Weight {
	int W[3];	/*存取權重值*/
	char sign;
};


int main()
{
	int s[3];/*初始權重*/
	int sum = 0;/*相加後的結果*/
	int count = 0;/*正確的次數*/
	int cycle = 1;
	struct Weight I[4];/*權重的數量*/

	cout << "初始權重:" << endl;
	for (int i = 0; i < 3; i++)
	{
		cin >> s[i];
	}
	cout <<endl<< "已知權重: ";
	for (int i = 0; i < 4; i++)/*輸入權重的值*/
	{
		cout << endl << "I" << i + 1 << ": "<<endl;
		for (int j = 0; j < 3; j++)
		{
			cin >> I[i].W[j];
		}
		cin >> I[i].sign;
	}
	cout << "        Chosen Instance  Current Weight Vector" << endl;
	cout << "Cycle 0       (" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
	while (count != 4)/*正確次數為達到4次就繼續迴圈*/
	{
		count = 0;
		for (int i = 0; i < 4; i++) 
		{
			sum = 0;
			for (int j = 0; j < 3; j++)
			{
				sum += s[j] * I[i].W[j];
			}
			if (sum <= 0 && I[i].sign == '+')/*如果符號是正的,結果小於等於0*/
			{
				for (int j = 0; j < 3; j++)
				{
					s[j] += I[i].W[j];
				}
			}
			else if (sum >= 0 && I[i].sign == '-')/*如果符號是負的,結果大於等於0*/
			{
				for (int j = 0; j < 3; j++)
				{
					s[j] -= I[i].W[j];
				}
			}
			else
			{
				count++;/*正確次數+1*/
			}
			if (count != 4)/*如果不等於4的話,會一直迴圈遞增cycle*/
			{
				cout << "Cycle " << cycle << "  (" << I[i].W[0] << "," << I[i].W[1] << "," << I[i].W[2] << "," << I[i].sign << ")";
				cout << "     (" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
				cout << endl;
				cycle++;
			}
		}
	}
	cout << "答案為:" << "(" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
	cout << endl;
	system("pause");		
}