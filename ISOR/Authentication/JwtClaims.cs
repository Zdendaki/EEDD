namespace ISOR.Authentication
{
    public static class JwtClaims
    {
        /// <summary>
        /// Issuer
        /// </summary>
        public const string Issuer = "iss";
        /// <summary>
        /// Subject
        /// </summary>
        public const string Subject = "sub";
        /// <summary>
        /// Audience
        /// </summary>
        public const string Audience = "aud";
        /// <summary>
        /// Expiration Time
        /// </summary>
        public const string ExpirationTime = "exp";
        /// <summary>
        /// Not Before
        /// </summary>
        public const string NotBefore = "nbf";
        /// <summary>
        /// Issued At
        /// </summary>
        public const string IssuedAt = "iat";
        /// <summary>
        /// JWT ID
        /// </summary>
        public const string JwtID = "jti";
        /// <summary>
        /// Full name
        /// </summary>
        public const string Name = "name";
        /// <summary>
        /// Given name or first name
        /// </summary>
        public const string GivenName = "given_name";
        /// <summary>
        /// Surname or last name
        /// </summary>
        public const string FamilyName = "family_name";
        /// <summary>
        /// Middle name
        /// </summary>
        public const string MiddleName = "middle_name";
        /// <summary>
        /// Casual name
        /// </summary>
        public const string Nickname = "nickname";
        /// <summary>
        /// Shorthand name by which the End-User wishes to be referred to
        /// </summary>
        public const string PreferredUsername = "preferred_username";
        /// <summary>
        /// Profile page URL
        /// </summary>
        public const string Profile = "profile";
        /// <summary>
        /// Profile picture URL
        /// </summary>
        public const string Picture = "picture";
        /// <summary>
        /// Web page or blog URL
        /// </summary>
        public const string Website = "website";
        /// <summary>
        /// Preferred e-mail address
        /// </summary>
        public const string Email = "email";
        /// <summary>
        /// True if the e-mail address has been verified; otherwise false
        /// </summary>
        public const string EmailVerified = "email_verified";
        /// <summary>
        /// Gender
        /// </summary>
        public const string Gender = "gender";
        /// <summary>
        /// Birthday
        /// </summary>
        public const string BirthDate = "birthdate";
        /// <summary>
        /// Time zone
        /// </summary>
        public const string TimeZone = "zoneinfo";
        /// <summary>
        /// Locale
        /// </summary>
        public const string Locale = "locale";
        /// <summary>
        /// Preferred telephone number
        /// </summary>
        public const string PhoneNumber = "phone_number";
        /// <summary>
        /// True if the phone number has been verified; otherwise false
        /// </summary>
        public const string PhoneNumberVerified = "phone_number_verified";
        /// <summary>
        /// Preferred postal address
        /// </summary>
        public const string Address = "address";
        /// <summary>
        /// Time the information was last updated
        /// </summary>
        public const string UploadedAt = "uploaded_at";
        /// <summary>
        /// Authorized party - the party to which the ID Token was issued
        /// </summary>
        public const string AuthorizedPArty = "azp";
        /// <summary>
        /// Value used to associate a Client session with an ID Token
        /// </summary>
        public const string Nonce = "nonce";
        /// <summary>
        /// Time when the authentication occurred
        /// </summary>
        public const string AuthTime = "auth_time";
        /// <summary>
        /// Access Token hash value
        /// </summary>
        public const string AccessTokenHash = "at_hash";
        /// <summary>
        /// Code hash value
        /// </summary>
        public const string CodeHashValue = "c_hash";
        /// <summary>
        /// Authentication Context Class Reference
        /// </summary>
        public const string AutheContextClassRef = "acr";
        /// <summary>
        /// Public key used to check the signature of an ID Token
        /// </summary>
        public const string SignatureKey = "sub_jwk";
        /// <summary>
        /// Confirmation
        /// </summary>
        public const string Confirmation = "cnf";
        /// <summary>
        /// SIP From tag header field parameter value
        /// </summary>
        public const string SipFromTag = "sip_from_tag";
        /// <summary>
        /// SIP Date header field value
        /// </summary>
        public const string SipDate = "sip_date";
        /// <summary>
        /// SIP Call-Id header field value
        /// </summary>
        public const string SipCallID = "sipp_callid";
        /// <summary>
        /// SIP CSeq numeric header field parameter value
        /// </summary>
        public const string SipCSeqNumber = "sip_cseq_num";
        /// <summary>
        /// SIP Via branch header field parameter value
        /// </summary>
        public const string SipViaBranch = "sip_via_branch";
        /// <summary>
        /// Originating Identity String
        /// </summary>
        public const string OriginIdentity = "orig";
        /// <summary>
        /// Destination Identity String
        /// </summary>
        public const string DestIdentity = "dest";
        /// <summary>
        /// Media Key Fingerprint String
        /// </summary>
        public const string MediaKeyFingerprint = "mky";
        /// <summary>
        /// Security Events
        /// </summary>
        public const string SecurityEvents = "events";
        /// <summary>
        /// Time of Event
        /// </summary>
        public const string TimeOfEvent = "toe";
        /// <summary>
        /// Transaction Identifier
        /// </summary>
        public const string TransactionID = "txn";
        /// <summary>
        /// Resource Priority Header Authorization
        /// </summary>
        public const string ResourcePriorityHeader = "rph";
        /// <summary>
        /// Session ID
        /// </summary>
        public const string SessionID = "sid";
        /// <summary>
        /// Vector of Trust value
        /// </summary>
        public const string VectorOfTrust = "vot";
        /// <summary>
        /// Vector of Trust trustmark URL
        /// </summary>
        public const string VectorOfTrustMarkURL = "vtm";
        /// <summary>
        /// Attestation level as defined in SHAKEN framework
        /// </summary>
        public const string AttestationLevel = "attest";
        /// <summary>
        /// Originating Identifier as defined in SHAKEN framework
        /// </summary>
        public const string OriginatingID = "origid";
        /// <summary>
        /// Actor
        /// </summary>
        public const string Actor = "act";
        /// <summary>
        /// Client Identifier
        /// </summary>
        public const string ClientID = "client_id";
        /// <summary>
        /// Authorized Actor - the party that is authorized to become the actor
        /// </summary>
        public const string AuthorizedActor = "may_act";
        /// <summary>
        /// jCard data
        /// </summary>
        public const string JCard = "jcard";
        /// <summary>
        /// Number of API requests for which the access token can be used
        /// </summary>
        public const string ApiRequestsCount = "at_use_nbr";
        /// <summary>
        /// Diverted Target of a Call
        /// </summary>
        public const string DivertedTarget = "div";
        /// <summary>
        /// Original PASSporT (in Full Form)
        /// </summary>
        public const string OriginalPassport = "opt";
        /// <summary>
        /// Verifiable Credential as specified in the W3C Recommendation
        /// </summary>
        public const string VerifiableCredential = "vc";
        /// <summary>
        /// Verifiable Presentation in the W3C Recommendation
        /// </summary>
        public const string VerifiablePresentation = "vp";
        /// <summary>
        /// SUP Priority header field
        /// </summary>
        public const string SipPriorityHeader = "sph";
        /// <summary>
        /// The ACE profile a token is supposed to be used with
        /// </summary>
        public const string AceProfile = "ace_profile";
        /// <summary>
        /// "client-nonce". A nonce previously provided to the AS by the RS via the client. Used to verify token freshness when the RS cannot synchronize its clock with the AS.
        /// </summary>
        public const string ClientNonce = "cnonce";
        /// <summary>
        /// "Expires in". Lifetime of the token in seconds from the time the RS first sees it. Used to implement a weaker form of token expiration and cannot synchronize their internal clocks.
        /// </summary>
        public const string ExpiresInLifetime = "exi";
        /// <summary>
        /// Roles
        /// </summary>
        public const string Roles = "roles";
        /// <summary>
        /// Groups
        /// </summary>
        public const string Groups = "groups";
        /// <summary>
        /// Entitlements
        /// </summary>
        public const string Entitlements = "entitlements";
        /// <summary>
        /// Token introspection response
        /// </summary>
        public const string TokenIntrospection = "token_introspection";
        /// <summary>
        /// CDNI Claim Set Version
        /// </summary>
        public const string CDNIClaimsVersion = "cdniv";
        /// <summary>
        /// CDNI Critical Claims Set
        /// </summary>
        public const string CDNICriticalClaims = "cdnicrit";
        /// <summary>
        /// CDNI IP Address
        /// </summary>
        public const string CDNIIPAddress = "cdniip";
        /// <summary>
        /// CDNI URI Container
        /// </summary>
        public const string CDNIURI = "cdniuc";
        /// <summary>
        /// CDNI Expiration Time setting for Signed Token Renewal
        /// </summary>
        public const string CDNIExpirationTime = "cdniets";
        /// <summary>
        /// CDNI Signed Token Transport Method for Signed Token Renewal
        /// </summary>
        public const string CDNISignedTokenTransportMethod = "cdnistt";
        /// <summary>
        /// CDNI Signed Token Depth
        /// </summary>
        public const string CDNISignedTokenDepth = "cdnistd";
        /// <summary>
        /// Signature Validation Token
        /// </summary>
        public const string SignatureValidationToken = "sig_val_claims";
    }
}
