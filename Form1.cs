namespace ShoppingCart
{
    public partial class Form1 : Form
    {
        private Item itemCoffee;
        private Item itemGreenTea;
        private Item itemNoodle;
        private Item itemPizza;

        public Form1()
        {
            InitializeComponent();
            itemCoffee = new Item("Coffee", 75);
            itemGreenTea = new Item("Green Tea", 55);
            itemNoodle = new Item("Noodle", 80);
            itemPizza = new Item("Pizza", 150);

            UpdateItemDisplay();
        }
        private void UpdateItemDisplay()
        {
            tbCoffeePrice.Text = itemCoffee.Price.ToString();
            tbCoffeeQuantity.Text = itemCoffee.Quantity.ToString();

            tbGreenTeaPrice.Text = itemGreenTea.Price.ToString();
            tbGreenTeaQuantity.Text = itemGreenTea.Quantity.ToString();

            tbNoodlePrice.Text = itemNoodle.Price.ToString();
            tbNoodleQuantity.Text = itemNoodle.Quantity.ToString();

            tbPizzaPrice.Text = itemPizza.Price.ToString();
            tbPizzaQuantity.Text = itemPizza.Quantity.ToString();
        }

        double getPriceFromSelectedItems()
        {
            string strCoffeePrice = tbCoffeePrice.Text;
            string strCoffeeQuantity = tbCoffeeQuantity.Text;

            int iCoffeePrice = 0;
            int iCoffeeQuantity = 0;
            try
            {
                if (chbCoffee.Checked)
                {
                    iCoffeePrice = int.Parse(strCoffeePrice);
                    iCoffeeQuantity = int.Parse(strCoffeeQuantity);
                }
            }
            catch (Exception ex)
            {
                iCoffeePrice = 0;
                iCoffeeQuantity = 0;
            }
            int iPrice = iCoffeePrice * iCoffeeQuantity;
            double dTotal = getDiscountPrice(iPrice, "BEV");
            return dTotal;
        }
        
        private double GetItemTotal(Item item)
        {
            if (!item.IsChecked)
                return 0;

            return item.Price * item.Quantity;
        }
        double getDiscountPrice(int pTotal, string pType = "ALL")
        {
            double dDiscountBev = 0;
            if (chbDiscountBev.Checked)
                dDiscountBev = int.Parse(tbDiscountBev.Text);
            return pTotal - (pTotal * dDiscountBev / 100);
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
                try
                {
                    double dCash = double.Parse(tbCash.Text);
                    double dBeverageTotal = 0;
                    double dFoodTotal = 0;

                    if (itemCoffee.IsChecked)
                    {
                        dBeverageTotal += GetItemTotal(tbCoffeePrice.Text, tbCoffeeQuantity.Text);
                    }
                    if (itemGreenTea.IsChecked)
                    {
                        dBeverageTotal += GetItemTotal(tbGreenTeaPrice.Text, tbGreenTeaQuantity.Text);
                    }

                    if (itemNoodle.IsChecked)
                    {
                        dFoodTotal += GetItemTotal(tbNoodlePrice.Text, tbNoodleQuantity.Text);
                    }
                    if (itemPizza.IsChecked)
                    {
                        dFoodTotal += GetItemTotal(tbPizzaPrice.Text, tbPizzaQuantity.Text);
                    }

                    double dGrandTotal = dBeverageTotal + dFoodTotal;

                    double dTotalDiscount = CalculateTotalDiscount(dBeverageTotal, dFoodTotal, dGrandTotal);

                    dGrandTotal -= dTotalDiscount;

                    if (dCash < dGrandTotal)
                    {
                        MessageBox.Show("เงินสดไม่เพียงพอ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    double dChange = dCash - dGrandTotal;

                    tbTotal.Text = dGrandTotal.ToString("F2");
                    tbChange.Text = dChange.ToString("F2");

                    CalculateChangeDenominations(dChange);
                }
                catch (FormatException)
                {
                    MessageBox.Show("กรุณากรอกข้อมูลตัวเลขให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }  

        private double GetItemTotal(string priceText, string quantityText)
        {
            double price = 0, quantity = 0;
            try
            {
                price = double.Parse(priceText);
                quantity = double.Parse(quantityText);
            }
            catch (Exception)
            {
                price = 0;
                quantity = 0;
            }
            return price * quantity;
        }
        private double CalculateTotalDiscount(double dBeverageTotal, double dFoodTotal, double dGrandTotal)
        {
            double dDiscountBev = chbDiscountBev.Checked ? double.Parse(tbDiscountBev.Text) : 0;
            double dDiscountFood = chbfood.Checked ? double.Parse(tbFood.Text) : 0;
            double dDiscountAll = chbAll.Checked ? double.Parse(tbAll.Text) : 0;

            return (dBeverageTotal * dDiscountBev / 100) +
                   (dFoodTotal * dDiscountFood / 100) +
                   (dGrandTotal * dDiscountAll / 100);
        }

        private void CalculateChangeDenominations(double change)
        {
            double[] denominations = { 1000, 500, 100, 50, 20, 10, 5, 1, 0.50, 0.25 };
            int[] changeCount = new int[denominations.Length];
            double remainChange = change;

            for (int i = 0; i < denominations.Length; i++)
            {
                changeCount[i] = (int)(remainChange / denominations[i]);
                remainChange %= denominations[i];
            }

            tb1000.Text = changeCount[0].ToString();
            tb500.Text = changeCount[1].ToString();
            tb100.Text = changeCount[2].ToString();
            tb50.Text = changeCount[3].ToString();
            tb20.Text = changeCount[4].ToString();
            tb10.Text = changeCount[5].ToString();
            tb5.Text = changeCount[6].ToString();
            tb1.Text = changeCount[7].ToString();
        }

        private void chbCoffee_CheckedChanged(object sender, EventArgs e)
        {
            itemCoffee.IsChecked = chbCoffee.Checked;
        }

        private void chbGreenTea_CheckedChanged(object sender, EventArgs e)
        {
            itemGreenTea.IsChecked = chbGreenTea.Checked;
        }

        private void chbNoodle_CheckedChanged(object sender, EventArgs e)
        {
            itemNoodle.IsChecked = chbNoodle.Checked;
        }

        private void chbPizza_CheckedChanged(object sender, EventArgs e)
        {
            itemPizza.IsChecked = chbPizza.Checked;
        }
    }

    namespace ShoppingCart
    {
        internal class Item {

    }
    }
    }
