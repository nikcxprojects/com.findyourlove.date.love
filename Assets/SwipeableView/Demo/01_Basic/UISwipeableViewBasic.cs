using System.Collections.Generic;

namespace SwipeableView
{
    public class UISwipeableViewBasic : UISwipeableView<BasicCardData>
    {

        public bool cardsIsOver()
        {
            return CardsIsOver();
        }
        
        public void UpdateData(List<BasicCardData> data)
        {
            Initialize(data);
        }
    }
}