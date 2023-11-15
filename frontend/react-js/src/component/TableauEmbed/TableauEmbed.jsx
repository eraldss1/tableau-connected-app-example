import React, { useEffect, useState, useRef } from "react";
import { Helmet, HelmetProvider } from "react-helmet-async";

const TableauEmbed = (props) => {
  const [viz, setViz] = useState();
  const [token, setToken] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const vizRef = useRef(null);

  const loadViz = () => {
    setViz(
      <tableau-viz
        ref={vizRef}
        id="tableauViz"
        src={
          "https://prod-apnortheast-a.online.tableau.com/t/visidataversa/views/DashboardPedas/DashboardPedas"
        }
        device={"desktop"}
        hide-tabs={false}
        token={token}
        toolbar="hidden"
      />
    );
  };

  useEffect(() => {
    let isSubscribed = true;

    const fetchData = async () => {
      setLoading(true);

      const response = await fetch(
        `http://localhost:5000/token?username=${props.username}`
      );
      const data = await response.json();
      if (isSubscribed) {
        setToken(data.token);
      }

      setLoading(false);
    };

    fetchData().catch((err) => setError(err));

    return () => (isSubscribed = false);
  }, [props.username]);

  useEffect(() => {
    if (token) {
      loadViz();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [token]);

  if (loading) return "Loading...";
  if (error) return "Error! " + JSON.stringify(error);
  return (
    <>
      <HelmetProvider>
        <Helmet>
          <script
            type="module"
            src="https://prod-apnortheast-a.online.tableau.com/javascripts/api/tableau.embedding.3.latest.min.js"
            async
          ></script>
        </Helmet>
        <div className="mx-0">{viz}</div>
      </HelmetProvider>
    </>
  );
};

export default TableauEmbed;
