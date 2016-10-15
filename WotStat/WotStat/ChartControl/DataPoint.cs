namespace WpfSampleBasicChart
{
    public class DataPoint:NotifierBase
    {
        private double m_Frequency = new double();
        public double Frequency
        {
            get { return m_Frequency; }
            set
            {
                SetProperty(ref m_Frequency, value);
            }
        }

        private double m_Value = new double();
        public double Value
        {
            get { return m_Value; }
            set
            {
                SetProperty(ref m_Value, value);
            }
        }
    }
}
