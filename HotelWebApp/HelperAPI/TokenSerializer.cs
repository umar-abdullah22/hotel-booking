namespace HotelWebApp.HelperAPI
{
    public  class TokenSerializer
    {
        public string Token { get; set; }
        public DateTime expireDate { get; set; }
        public static  string TokenSaved { get; set; }
        public static  string ClaimName { get; set; }
        public static  string ClaimUserId { get; set; }

    }
}
